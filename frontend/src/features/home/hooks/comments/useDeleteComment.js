import { useState, useCallback } from 'react';
import { commentsService } from '../services/commentsService';

export function useDeleteComment() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const deleteComment = useCallback(async (commentId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await commentsService.deleteComment(commentId, userId);
    
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
    deleteComment, 
    isLoading, 
    error,
    result,
    reset 
  };
}

