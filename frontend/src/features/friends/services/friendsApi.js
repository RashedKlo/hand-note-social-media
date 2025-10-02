import { apiClient } from "../../../services/api/client";

export const friendsApi = {
  /**
   * Send friend request
   */
  sendFriendRequest: async (requesterId, recipientId) => {
    const response = await apiClient.post('/friendships', {
      requesterId,
      recipientId,
    });
    return response.data;
  },

  /**
   * Accept friend request
   */
  acceptFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/accept`, {
      userId,
    });
    return response.data;
  },

  /**
   * Decline friend request
   */
  declineFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/decline`, {
      userId,
    });
    return response.data;
  },

  /**
   * Cancel friend request
   */
  cancelFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/cancel`, {
      userId,
    });
    return response.data;
  },

  /**
   * Block user
   */
  blockUser: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/block`, {
      userId,
    });
    return response.data;
  },

  /**
   * Check friendship status
   */
  checkFriendshipStatus: async (userId1, userId2) => {
    const response = await apiClient.get('/friendships/check', {
      userId1,
      userId2,
    });
    return response.data;
  },

  /**
   * Get pending friend requests
   */
  loadFriendRequests: async (userId, page = 1, limit = 10) => {


    const response = await apiClient.get(`/friendships/${userId}/requests`,
      {
        params:{page,limit}
      }
    );
    return response.data;
  },

  /**
   * Get user friends
   */
  loadFriends: async (userId, page = 1, limit = 10) => {
   

    const response = await apiClient.get(`/friendships/${userId}/friends`,{
      params:{
        page,limit
      }
    });
    return response;
  },

  /**
   * Search user friends by name
   */
  searchUserFriends: async (userId, filter = '', page = 1, limit = 10) => {

    const response = await apiClient.get(`/friendships/${userId}/friends/search`,{
      params:{
        filter,page,limit
      }
    });
    return response.data;
  },
};


