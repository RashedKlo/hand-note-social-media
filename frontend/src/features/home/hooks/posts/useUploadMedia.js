import { useState, useCallback } from 'react';
import { mediaService } from '../services/mediaService';

export function useUploadMedia() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const uploadMedia = useCallback(async (userId, filePaths) => {
    setIsLoading(true);
    setError(null);
    
    const res = await mediaService.uploadMedia(userId, filePaths);
    
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
    uploadMedia, 
    isLoading, 
    error,
    result,
    reset 
  };
}