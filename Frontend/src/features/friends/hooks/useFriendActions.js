import { useState, useCallback } from 'react';
import { friendsService } from '../services';

export function useFriendActions() {
  const [actionLoading, setActionLoading] = useState({});
  const [actionError, setActionError] = useState(null);

  // Set loading state for specific action
  const setLoading = useCallback((key, loading) => {
    setActionLoading(prev => ({
      ...prev,
      [key]: loading
    }));
  }, []);

  // Send friend request
  const sendFriendRequest = useCallback(async (recipientId) => {
    const key = `send_${recipientId}`;
    setLoading(key, true);
    setActionError(null);

    try {
      const result = await friendsService.sendFriendRequest(recipientId);
      return result.success;
    } catch (err) {
      setActionError('Failed to send friend request');
      return false;
    } finally {
      setLoading(key, false);
    }
  }, [setLoading]);

  // Remove friend
  const removeFriend = useCallback(async (friendshipId, friendName) => {
    const key = `remove_${friendshipId}`;
    setLoading(key, true);
    setActionError(null);

    try {
      const result = await friendsService.removeFriend(friendshipId, friendName);
      return result.success;
    } catch (err) {
      setActionError('Failed to remove friend');
      return false;
    } finally {
      setLoading(key, false);
    }
  }, [setLoading]);

  // Clear error
  const clearError = useCallback(() => {
    setActionError(null);
  }, []);

  return {
    actionLoading,
    actionError,
    sendFriendRequest,
    removeFriend,
    clearError
  };
}
