import { useState, useCallback } from 'react';
import { privacyService } from '../services/privacyService';

export function useUpdatePrivacySettings() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const updateSettings = useCallback(async (userId, settings) => {
    setIsLoading(true);
    setError(null);
    
    const res = await privacyService.updatePrivacySettings(userId, settings);
    
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
    updateSettings, 
    isLoading, 
    error,
    result,
    reset 
  };
}
