import { useState, useCallback } from 'react';
import { postsService } from '../services/postsService';

export function useDeletePost() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const deletePost = useCallback(async (postId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await postsService.deletePost(postId, userId);
    
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
    deletePost, 
    isLoading, 
    error,
    result,
    reset 
  };
}