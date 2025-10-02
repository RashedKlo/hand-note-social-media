import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useBlockUser() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState(null);

  const blockUser = useCallback(async (friendshipId, userId) => {
    setIsLoading(true);
    const res = await friendsService.blockUser(friendshipId, userId);
    setResult(res);
    setIsLoading(false);
    return res;
  }, []); 

  return { blockUser, isLoading, result };
}
