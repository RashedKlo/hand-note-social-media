import { useState, useCallback } from 'react';
import { conversationsService } from '../services/conversationsService';

export function useCheckUserMembership() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const checkMembership = useCallback(async (conversationId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await conversationsService.checkUserMembership(conversationId, userId);
    
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
    checkMembership, 
    isLoading, 
    error,
    result,
    reset 
  };
}
