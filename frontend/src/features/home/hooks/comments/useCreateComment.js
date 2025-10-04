import { useState, useCallback } from 'react';
import { commentsService } from '../services/commentsService';

export function useCreateComment() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createComment = useCallback(async (commentData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await commentsService.createComment(commentData);
    
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
    createComment, 
    isLoading, 
    error,
    result,
    reset 
  };
}