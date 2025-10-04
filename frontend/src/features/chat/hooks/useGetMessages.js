import { useState, useCallback } from 'react';
import { messagesService } from '../services/messagesService';

export function useGetMessages() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [messages, setMessages] = useState([]);

  const getMessages = useCallback(async (conversationId, currentUserId, options = {}) => {
    setIsLoading(true);
    setError(null);
    
    const res = await messagesService.getMessages(conversationId, currentUserId, options);
    
    if (!res.success) {
      setError(res.message);
    } else {
      setMessages(res.data || []);
    }
    
    setIsLoading(false);
    return res;
  }, []);

  const reset = useCallback(() => {
    setError(null);
    setMessages([]);
  }, []);

  return { 
    getMessages, 
    isLoading, 
    error,
    messages,
    reset 
  };
}