import { useState, useCallback } from "react";
import { notificationsService } from "../services/notificationsService";

export function useGetUnreadCount() {
  const [isLoading, setIsLoading] = useState(false);
  const [unreadCount, setUnreadCount] = useState(0);
  const [error, setError] = useState(null);

  const getUnreadCount = useCallback(async (userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await notificationsService.getUnreadCount(userId);
    
    if (res.success) {
      setUnreadCount(res.data || 0);
    } else {
      setError(res.message);
    }
    
    setIsLoading(false);
    return res;
  }, []);

  const reset = useCallback(() => {
    setUnreadCount(0);
    setError(null);
  }, []);

  return { 
    getUnreadCount, 
    isLoading, 
    unreadCount,
    error,
    reset 
  };
}