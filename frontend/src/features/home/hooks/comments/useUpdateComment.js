import { useState, useCallback } from 'react';
import { commentsService } from '../services/commentsService';

export function useUpdateComment() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const updateComment = useCallback(async (commentId, commentData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await commentsService.updateComment(commentId, commentData);
    
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
    updateComment, 
    isLoading, 
    error,
    result,
    reset 
  };
}