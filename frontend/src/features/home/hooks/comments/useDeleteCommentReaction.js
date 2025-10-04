import { useState, useCallback } from 'react';
import { commentsService } from '../services/commentsService';

export function useDeleteCommentReaction() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const deleteReaction = useCallback(async (commentId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await commentsService.deleteCommentReaction(commentId, userId);
    
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
