import { useState, useCallback } from 'react';
import { sessionsService } from '../services/sessionsService';

export function useLogoutAllSessions() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const logoutAll = useCallback(async (email) => {
    setIsLoading(true);
    setError(null);
    
    const res = await sessionsService.logoutAllSessions(email);
    
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
    logoutAll, 
    isLoading, 
    error,
    result,
    reset 
  };
}

