import { apiClient } from '../../../services/api/endpoints'

export const friendsApi = {
  /**
   * Search for users
   */
  searchFriends: async (query, page = 1, limit = 20) => {
    const params = new URLSearchParams({
      q: query,
      page: page.toString(),
      limit: limit.toString(),
      
    });
    
    const response = await apiClient.get(`/users/search?${params}`);
    return response.data;
  },

  /**
   * Load user's friends list
   */
  loadFriends: async (userId, page = 1, limit = 50) => {
    const params = new URLSearchParams({
      page: page.toString(),
      limit: limit.toString()
    });
    
    const response = await apiClient.get(`/friendships/${userId}/friends?${params}`);
    return response.data;
  },

  /**
   * Load received friend requests
   */
  loadReceivedRequests: async (userId, page = 1, limit = 20) => {
    const params = new URLSearchParams({
      page: page.toString(),
      limit: limit.toString(),
      type: 'received'
    });
    
    const response = await apiClient.get(`/friendships/${userId}/requests?${params}`);
    return response.data;
  },


  /**
   * Send friend request
   */
  sendFriendRequest: async (requesterId, recipientId) => {
    const response = await apiClient.post('/friendships', {
      requesterId,
      recipientId
    });
    return response.data;
  },

  /**
   * Accept friend request
   */
  acceptFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/accept`, {
      userId
    });
    return response.data;
  },

  /**
   * Decline friend request
   */
  declineFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/decline`, {
      userId
    });
    return response.data;
  },

  /**
   * Cancel sent friend request
   */
  cancelFriendRequest: async (friendshipId, userId) => {
    const response = await apiClient.put(`/friendships/${friendshipId}/cancel`, {
      userId
    });
    return response.data;
  },

  /**
   * Remove friend
   */
  removeFriend: async (friendshipId, userId) => {
    const response = await apiClient.delete(`/friendships/${friendshipId}`, {
      data: { userId }
    });
    return response.data;
  },



};
