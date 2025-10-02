import { useState, useCallback } from 'react';
import { friendsService } from '../services/friendsService';

export function useSendFriendRequest() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const sendFriendRequest = useCallback(async (requesterId, recipientId) => {
    setIsLoading(true);
    setError(null);
    
    const result = await friendsService.sendFriendRequest(requesterId, recipientId);
    
    if (!result.success) {
      setError(result.message);
    }
    
    setIsLoading(false);
    return result;
  }, []);

  const reset = useCallback(() => {
    setError(null);
  }, []);

  return { 
    sendFriendRequest, 
    isLoading, 
    error,
    reset 
  };
}