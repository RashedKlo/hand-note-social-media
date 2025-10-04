import { useState, useCallback } from 'react';
import { privacyService } from '../services/privacyService';

export function useResetPrivacySettings() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const resetSettings = useCallback(async (userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await privacyService.resetPrivacySettings(userId);
    
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
    resetSettings, 
    isLoading, 
    error,
    result,
    reset 
  };
}
