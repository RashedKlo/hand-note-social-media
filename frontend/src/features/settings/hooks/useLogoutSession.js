import { useState, useCallback } from 'react';
import { sessionsService } from '../services/sessionsService';

export function useLogoutSession() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const logout = useCallback(async (email, sessionId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await sessionsService.logoutSession(email, sessionId);
    
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
    logout, 
    isLoading, 
    error,
    result,
    reset 
  };
}

