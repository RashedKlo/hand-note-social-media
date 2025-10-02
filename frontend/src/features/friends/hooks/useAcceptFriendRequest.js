import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useAcceptFriendRequest() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);
 
  const acceptFriendRequest = useCallback(async (friendshipId, userId) => {
    setIsLoading(true);
    const res = await friendsService.acceptFriendRequest(friendshipId, userId);
    setResult(res);
    setIsLoading(false);
    return res;
  }, []);

  return { acceptFriendRequest, isLoading, result };
}
