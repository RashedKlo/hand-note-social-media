import { useState, useEffect, useCallback } from 'react';
import { friendsService } from '../services';
import { useSocialStore } from '../../../store/socialStore';

export function useFriendsList(userId) {
  const [friends, setFriends] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [hasMore, setHasMore] = useState(false);
  const [page, setPage] = useState(1);
  
  const { updateFriends } = useSocialStore();

  // Load friends
  const loadFriends = useCallback(async (pageNum = 1, refresh = false) => {
    if (loading) return;
    
    setLoading(true);
    setError(null);

    try {
      const result = await friendsService.loadFriends(userId, pageNum, 50);
      
      if (result.success) {
        setFriends(prev => {
          const newFriends = refresh || pageNum === 1 
            ? result.data 
            : [...prev, ...result.data];
          
          // Update global store
          updateFriends(newFriends);
          return newFriends;
        });
        
        setHasMore(result.pagination?.hasMore || false);
        setPage(pageNum);
      } else {
        setError(result.message);
      }
    } catch (err) {
      setError('Failed to load friends');
    } finally {
      setLoading(false);
    }
  }, [userId, loading, updateFriends]);

  // Load more friends
  const loadMore = useCallback(() => {
    if (!loading && hasMore) {
      loadFriends(page + 1);
    }
  }, [loadFriends, loading, hasMore, page]);

  // Refresh friends list
  const refresh = useCallback(() => {
    loadFriends(1, true);
  }, [loadFriends]);

  // Remove friend from local state
  const removeFriendFromList = useCallback((friendshipId) => {
    setFriends(prev => prev.filter(friend => friend.friendshipId !== friendshipId));
  }, []);

  // Initial load
  useEffect(() => {
    if (userId) {
      loadFriends(1);
    }
  }, [userId, loadFriends]);

  return {
    friends,
    loading,
    error,
    hasMore,
    loadMore,
    refresh,
    removeFriendFromList
  };
}