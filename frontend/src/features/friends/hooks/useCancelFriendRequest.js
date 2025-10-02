import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useCancelFriendRequest() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);

  const cancelFriendRequest = useCallback(async (friendshipId, userId) => {
    setIsLoading(true);
    const res = await friendsService.cancelFriendRequest(friendshipId, userId);
    setResult(res);
    setIsLoading(false);
    return res;
  }, []);

  return { cancelFriendRequest, isLoading, result };
}
