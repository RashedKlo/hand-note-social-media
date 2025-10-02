import { handleApi } from "../../../services/api/apiHandler";
import { validateDifferentUsers, validateIds, validatePagination, validateSearchFilter } from "../utils/friendsValidation";
import { friendsApi } from "./friendsApi";


export const friendsService = {
  /**
   * Send friend request
   */
  sendFriendRequest: async (requesterId, recipientId) =>
    handleApi(
      () => friendsApi.sendFriendRequest(requesterId, recipientId),
      () => validateIds(requesterId, recipientId)|| 
        validateDifferentUsers(requesterId, recipientId)
    ),

  /**
   * Accept friend request
   */
  acceptFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.acceptFriendRequest(friendshipId, userId),
      () => validateIds(friendshipId, userId)
    ),

  /**
   * Decline friend request
   */
  declineFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.declineFriendRequest(friendshipId, userId),
      () => validateIds(friendshipId, userId)
    ),

  /**
   * Cancel friend request
   */
  cancelFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.cancelFriendRequest(friendshipId, userId),
      () => validateIds(friendshipId, userId)
    ),

  /**
   * Block user
   */
  blockUser: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.blockUser(friendshipId, userId),
      () => validateIds(friendshipId, userId)
    ),

  /**
   * Check friendship status
   */
  checkFriendshipStatus: async (userId1, userId2) =>
    handleApi(
      () => friendsApi.checkFriendshipStatus(userId1, userId2),
      () => validateIds(userId1, userId2)|| 
        validateDifferentUsers(userId1, userId2)
    ),

  /**
   * Load pending friend requests
   */
  loadFriendRequests: async (userId, page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.loadFriendRequests(userId, page, limit),
      () => validateIds(userId) || validatePagination(page, limit)
    ),

  /**
   * Load user friends
   */
  loadFriends: async (userId, page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.loadFriends(userId, page, limit),
      () => validateIds(userId) || validatePagination(page, limit)
    ),

  /**
   * Search user friends
   */
  searchUserFriends: async (userId, filter = '', page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.searchUserFriends(userId, filter, page, limit),
      () => validateIds(userId) || validatePagination(page, limit)||validateSearchFilter(filter),
    ),
};