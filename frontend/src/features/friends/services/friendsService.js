import { handleApi } from "../../../services/api/apiHandler";
import { validateID, validateName, validatePagination } from "../../../utils/Helpers";
import { validateDifferentUsers } from "../utils/friendsValidation";
import { friendsApi } from "./friendsApi";


export const friendsService = {
  /**
   * Send friend request
   */
  sendFriendRequest: async (requesterId, recipientId) =>
    handleApi(
      () => friendsApi.sendFriendRequest(requesterId, recipientId),
      () => validateID(requesterId,"Invalid requester ID")|| validateID(recipientId,"Invalid recipient ID")
       || validateDifferentUsers(requesterId, recipientId)
    ),

  /**
   * Accept friend request
   */
  acceptFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.acceptFriendRequest(friendshipId, userId),
      () => validateID(friendshipId,"Invalid friendship ID"),validateName(userId,"Invalid user ID")
    ),

  /**
   * Decline friend request
   */
  declineFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.declineFriendRequest(friendshipId, userId),
      () => validateID(friendshipId,"Invalid friendship ID"),validateName(userId,"Invalid user ID")
    ),

  /**
   * Cancel friend request
   */
  cancelFriendRequest: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.cancelFriendRequest(friendshipId, userId),
      () => validateID(friendshipId,"Invalid friendship ID"),validateName(userId,"Invalid user ID")
    ),

  /**
   * Block user
   */
  blockUser: async (friendshipId, userId) =>
    handleApi(
      () => friendsApi.blockUser(friendshipId, userId),
      () => validateID(friendshipId,"Invalid friendship ID"),validateName(userId,"Invalid user ID")
    ),

  /**
   * Check friendship status
   */
  checkFriendshipStatus: async (userId1, userId2) =>
    handleApi(
      () => friendsApi.checkFriendshipStatus(userId1, userId2),
      () => validateID(userId1, "Invalid user ID 1")|| validateID(userId2, "Invalid user ID 2")||
        validateDifferentUsers(userId1, userId2)
    ),

  /**
   * Load pending friend requests
   */
  loadFriendRequests: async (userId, page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.loadFriendRequests(userId, page, limit),
      () => validateID(userId,"Invalid user ID") || validatePagination(page, limit,"Invalid pagination parameters")
    ),

  /**
   * Load user friends
   */
  loadFriends: async (userId, page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.loadFriends(userId, page, limit),
      () => validateID(userId) || validatePagination(page, limit)
    ),

  /**
   * Search user friends
   */
  searchUserFriends: async (userId, filter = '', page = 1, limit = 10) =>
    handleApi(
      () => friendsApi.searchUserFriends(userId, filter, page, limit),
      () => validateID(userId,"Invalid user ID") || validatePagination(page, limit)||validateName(filter,"Invalid search filter",0,100),
    ),
};