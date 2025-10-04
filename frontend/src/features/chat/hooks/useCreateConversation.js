import { useState, useCallback } from 'react';
import { conversationsService } from '../services/conversationsService';

export function useCreateConversation() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createConversation = useCallback(async (currentUserId, friendId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await conversationsService.createConversation(currentUserId, friendId);
    
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
    createConversation, 
    isLoading, 
    error,
    result,
    reset 
  };
}

