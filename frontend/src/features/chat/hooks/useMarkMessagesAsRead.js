import { useState, useCallback } from 'react';
import { messagesService } from '../services/messagesService';

export function useMarkMessagesAsRead() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const markAsRead = useCallback(async (conversationId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await messagesService.markAsRead(conversationId, userId);
    
    if (!res.success) {
      setError(res.message);
    }
    
    setResult(res);
    setIsLoading(false);
    return res;
  }, []);

  const reset = useCallback(() => {
    setError(null);
    setResult(null);
  }, []);

  return { 
    markAsRead, 
    isLoading, 
    error,
    result,
    reset 
  };
}
