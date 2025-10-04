import { useState, useCallback } from 'react';
import { postsService } from '../services/postsService';

export function useCreatePost() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createPost = useCallback(async (postData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await postsService.createPost(postData);
    
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
    createPost, 
    isLoading, 
    error,
    result,
    reset 
  };
}