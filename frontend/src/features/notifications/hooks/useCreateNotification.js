import { useState, useCallback } from "react";
import { notificationsService } from "../services/notificationsService";

export function useCreateNotification() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const createNotification = useCallback(async (notificationData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await notificationsService.createNotification(notificationData);
    
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
    createNotification, 
    isLoading, 
    error,
    result,
    reset 
  };
}