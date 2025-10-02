import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useLoadFriendRequests() {
  const [isLoading, setIsLoading] = useState(false);
  const [friendRequests, setRequests] = useState(null);

  const loadRequests = useCallback(async (userId, page = 1, limit = 10) => {
    setIsLoading(true);
    const res = await friendsService.loadFriendRequests(userId, page, limit);
    setRequests(res);
    setIsLoading(false);
    return res;
  }, []);
 
  return { loadRequests, isLoading, friendRequests };
}
