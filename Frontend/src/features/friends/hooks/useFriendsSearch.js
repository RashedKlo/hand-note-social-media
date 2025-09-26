import { useState, useCallback, useRef, useEffect } from 'react';
import { friendsService } from '../services';
import { debounce } from '../utils';

export function useFriendsSearch() {
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [hasMore, setHasMore] = useState(false);
  const [page, setPage] = useState(1);
  
  const abortControllerRef = useRef(null);
  const searchTimeoutRef = useRef(null);

  // Debounced search function
  const debouncedSearch = useCallback(
    debounce(async (query, pageNum = 1) => {
      if (!query.trim()) {
        setSearchResults([]);
        setHasMore(false);
        return;
      }

      // Cancel previous request
      if (abortControllerRef.current) {
        abortControllerRef.current.abort();
      }

      setLoading(true);
      setError(null);

      try {
        const result = await friendsService.searchUsers(query, pageNum, 20);
        
        if (result.success) {
          setSearchResults(prev => 
            pageNum === 1 ? result.data : [...prev, ...result.data]
          );
          setHasMore(result.pagination?.hasMore || false);
        } else {
          setError(result.message);
          if (pageNum === 1) setSearchResults([]);
        }
      } catch (err) {
        if (err.name !== 'AbortError') {
          setError('Search failed. Please try again.');
          if (pageNum === 1) setSearchResults([]);
        }
      } finally {
        setLoading(false);
      }
    }, 300),
    []
  );

  // Search function
  const search = useCallback((query) => {
    setSearchQuery(query);
    setPage(1);
    debouncedSearch(query, 1);
  }, [debouncedSearch]);

  // Load more results
  const loadMore = useCallback(() => {
    if (!loading && hasMore && searchQuery.trim()) {
      const nextPage = page + 1;
      setPage(nextPage);
      debouncedSearch(searchQuery, nextPage);
    }
  }, [loading, hasMore, searchQuery, page, debouncedSearch]);

  // Clear search
  const clearSearch = useCallback(() => {
    setSearchQuery('');
    setSearchResults([]);
    setError(null);
    setHasMore(false);
    setPage(1);
  }, []);

  // Cleanup on unmount
  useEffect(() => {
    return () => {
      if (abortControllerRef.current) {
        abortControllerRef.current.abort();
      }
      if (searchTimeoutRef.current) {
        clearTimeout(searchTimeoutRef.current);
      }
    };
  }, []);

  return {
    searchQuery,
    searchResults,
    loading,
    error,
    hasMore,
    search,
    loadMore,
    clearSearch
  };
}