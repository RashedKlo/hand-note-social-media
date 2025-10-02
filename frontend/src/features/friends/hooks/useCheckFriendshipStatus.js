import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useCheckFriendshipStatus() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);

  const checkStatus = useCallback(async (userId1, userId2) => {
    setIsLoading(true);
    const res = await friendsService.checkFriendshipStatus(userId1, userId2);
    setResult(res);
    setIsLoading(false);
    return res;
  }, []);

  return { checkStatus, isLoading, result };
}
