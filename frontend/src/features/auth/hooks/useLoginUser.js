import { useState, useCallback } from 'react';
import { authService } from '../services/authService';

export function useLoginUser() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const loginUser = useCallback(async (loginData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await authService.loginUser(loginData);
    
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
    loginUser, 
    isLoading, 
    error,
    result,
    reset 
  };
}