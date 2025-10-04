import { useState, useCallback } from 'react';
import { messagesService } from '../services/messagesService';

export function useSendMessage() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const sendMessage = useCallback(async (messageData) => {
    setIsLoading(true);
    setError(null);
    
    const res = await messagesService.sendMessage(messageData);
    
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
    sendMessage, 
    isLoading, 
    error,
    result,
    reset 
  };
}

