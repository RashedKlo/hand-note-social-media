import { useState, useEffect, useCallback } from 'react';
import { friendsService } from '../services';

export function useFriendRequests(userId) {
  const [receivedRequests, setReceivedRequests] = useState([]);
  const [sentRequests, setSentRequests] = useState([]);
  const [loading, setLoading] = useState({
    received: false,
    sent: false,
    action: {}
  });
  const [error, setError] = useState(null);

  // Load received requests
  const loadReceivedRequests = useCallback(async () => {
    setLoading(prev => ({ ...prev, received: true }));
    setError(null);

    try {
      const result = await friendsService.loadReceivedRequests(userId);
      if (result.success) {
        setReceivedRequests(result.data);
      } else {
        setError(result.message);
      }
    } catch (err) {
      setError('Failed to load friend requests');
    } finally {
      setLoading(prev => ({ ...prev, received: false }));
    }
  }, [userId]);

  // Load sent requests
  const loadSentRequests = useCallback(async () => {
    setLoading(prev => ({ ...prev, sent: true }));
    setError(null);

    try {
      const result = await friendsService.loadSentRequests(userId);
      if (result.success) {
        setSentRequests(result.data);
      } else {
        setError(result.message);
      }
    } catch (err) {
      setError('Failed to load sent requests');
    } finally {
      setLoading(prev => ({ ...prev, sent: false }));
    }
  }, [userId]);

  // Accept request
  const acceptRequest = useCallback(async (friendshipId) => {
    setLoading(prev => ({ 
      ...prev, 
      action: { ...prev.action, [friendshipId]: 'accepting' }
    }));

    try {
      const result = await friendsService.acceptFriendRequest(friendshipId);
      if (result.success) {
        // Remove from received requests
        setReceivedRequests(prev => 
          prev.filter(req => req.id !== friendshipId)
        );
        return true;
      } else {
        setError(result.message);
        return false;
      }
    } catch (err) {
      setError('Failed to accept request');
      return false;
    } finally {
      setLoading(prev => {
        const newAction = { ...prev.action };
        delete newAction[friendshipId];
        return { ...prev, action: newAction };
      });
    }
  }, []);

  // Decline request
  const declineRequest = useCallback(async (friendshipId) => {
    setLoading(prev => ({ 
      ...prev, 
      action: { ...prev.action, [friendshipId]: 'declining' }
    }));

    try {
      const result = await friendsService.declineFriendRequest(friendshipId);
      if (result.success) {
        // Remove from received requests
        setReceivedRequests(prev => 
          prev.filter(req => req.id !== friendshipId)
        );
        return true;
      } else {
        setError(result.message);
        return false;
      }
    } catch (err) {
      setError('Failed to decline request');
      return false;
    } finally {
      setLoading(prev => {
        const newAction = { ...prev.action };
        delete newAction[friendshipId];
        return { ...prev, action: newAction };
      });
    }
  }, []);

  // Cancel sent request
  const cancelRequest = useCallback(async (friendshipId) => {
    setLoading(prev => ({ 
      ...prev, 
      action: { ...prev.action, [friendshipId]: 'cancelling' }
    }));

    try {
      const result = await friendsService.cancelFriendRequest(friendshipId);
      if (result.success) {
        // Remove from sent requests
        setSentRequests(prev => 
          prev.filter(req => req.id !== friendshipId)
        );
        return true;
      } else {
        setError(result.message);
        return false;
      }
    } catch (err) {
      setError('Failed to cancel request');
      return false;
    } finally {
      setLoading(prev => {
        const newAction = { ...prev.action };
        delete newAction[friendshipId];
        return { ...prev, action: newAction };
      });
    }
  }, []);

  // Initial load
  useEffect(() => {
    if (userId) {
      loadReceivedRequests();
      loadSentRequests();
    }
  }, [userId, loadReceivedRequests, loadSentRequests]);

  return {
    receivedRequests,
    sentRequests,
    loading,
    error,
    acceptRequest,
    declineRequest,
    cancelRequest,
    refresh: () => {
      loadReceivedRequests();
      loadSentRequests();
    }
  };
}