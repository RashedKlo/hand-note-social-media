import { useState, useCallback } from 'react';
import { sessionsService } from '../services/sessionsService';

export function useGetActiveSessions() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [sessions, setSessions] = useState([]);

  const getActiveSessions = useCallback(async (userId) => {
    setIsLoading(true);
    setError(null);
    
    const res = await sessionsService.getActiveSessions(userId);
    
    if (!res.success) {
      setError(res.message);
    } else {
      setSessions(res.data?.sessions || []);
    }
    
    setIsLoading(false);
    return res;
  }, []);

  const reset = useCallback(() => {
    setError(null);
    setSessions([]);
  }, []);

  return { 
    getActiveSessions, 
    isLoading, 
    error,
    sessions,
    reset 
  };
}
