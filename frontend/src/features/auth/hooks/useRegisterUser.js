import { useState, useCallback } from 'react';
import { authService } from '../services/authService';

export function useRegisterUser() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const registerUser = useCallback(async (registrationData) => {
    setIsLoading(true);
    setError(null);
     
    const res = await authService.registerUser(registrationData);
    
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
    registerUser, 
    isLoading, 
    error,
    result,
    reset 
  };
}