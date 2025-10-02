import { useState, useCallback } from "react";
import { friendsService } from "../services/friendsService";
export function useLoadFriends() {
  const [isLoading, setIsLoading] = useState(false);
  const [friends, setFriends] = useState([]);

  const loadFriends = useCallback(async (userId, page = 1, limit = 10) => {
    setIsLoading(true);
    const res = await friendsService.loadFriends(userId, page, limit);
    setFriends(res);
    setIsLoading(false);
    return res;
  }, []);
 
  return { loadFriends, isLoading, friends };
}
