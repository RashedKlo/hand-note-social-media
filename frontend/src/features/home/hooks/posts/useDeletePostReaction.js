import { useState, useCallback } from 'react';
import { postsService } from '../services/postsService';

export function useDeletePostReaction() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const deleteReaction = useCallback(async (postId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await postsService.deletePostReaction(postId, userId);
    
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
    deleteReaction, 
    isLoading, 
    error,
    result,
    reset 
  };
}