import { useState, useCallback } from "react";
import { notificationsService } from "../services/notificationsService";

export function useGetUserNotifications() {
  const [isLoading, setIsLoading] = useState(false);
  const [notifications, setNotifications] = useState([]);
  const [error, setError] = useState(null);

  const getNotifications = useCallback(async (
    userId, 
    pageSize = 20, 
    pageNumber = 1, 
    includeRead = true
  ) => {
    setIsLoading(true);
    setError(null);
    
    const res = await notificationsService.getUserNotifications(
      userId, 
      pageSize, 
      pageNumber, 
      includeRead
    );
    
    if (res.success) {
      setNotifications(res.data || []);
    } else {
      setError(res.message);
    }
    
    setIsLoading(false);
    return res;
  }, []);

  const reset = useCallback(() => {
    setNotifications([]);
    setError(null);
  }, []);

  return { 
    getNotifications, 
    isLoading, 
    notifications,
    error,
    reset 
  };
}