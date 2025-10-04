import { useState, useCallback } from "react";
import { notificationsService } from "../services/notificationsService";

export function useMarkAsRead() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);
  const [error, setError] = useState(null);

  const markAsRead = useCallback(async (notificationId, userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await notificationsService.markAsRead(notificationId, userId);
    
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
    markAsRead, 
    isLoading, 
    result,
    error,
    reset 
  };
}