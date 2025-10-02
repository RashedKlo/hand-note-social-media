import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useSearchUserFriends() {
  const [isLoading, setIsLoading] = useState(false);
  const [friends, setFriends] = useState(null);

  const searchFriends = useCallback(async (userId, filter = "", page = 1, limit = 10) => {
    setIsLoading(true);
    const res = await friendsService.searchUserFriends(userId, filter, page, limit);
    setFriends(res);
    setIsLoading(false); 
    return res;
  }, []);

  return { searchFriends, isLoading, friends };
}
