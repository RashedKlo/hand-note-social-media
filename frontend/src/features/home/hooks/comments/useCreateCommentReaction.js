import { useState, useCallback } from 'react';
import { commentsService } from '../services/commentsService';

export function useCreateCommentReaction() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createReaction = useCallback(async (reactionData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await commentsService.createCommentReaction(reactionData);
    
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