import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useDeclineFriendRequest() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);

  const declineFriendRequest = useCallback(async (friendshipId, userId) => {
    setIsLoading(true);
    const res = await friendsService.declineFriendRequest(friendshipId, userId);
    setResult(res);
    setIsLoading(false);
    return res;
  }, []);
 
  return { declineFriendRequest, isLoading, result };
}
