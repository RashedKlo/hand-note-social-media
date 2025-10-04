import { useState, useCallback } from 'react';
import { postsService } from '../services/postsService';

export function useCreatePostReaction() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createReaction = useCallback(async (reactionData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await postsService.createPostReaction(reactionData);
    
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
    createReaction, 
    isLoading, 
    error,
    result,
    reset 
  };
}

