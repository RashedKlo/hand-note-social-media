import { friendsApi } from './friendsApi';
import { 
  handleApiError, 
  formatApiResponse, 
  getCurrentUserId,
  showNotification 
} from '../utils';

export const friendsService = {
  /**
   * Search for users with error handling and formatting
   */
  searchUsers: async (query, page = 1, limit = 20) => {
    try {
      if (!query || query.trim().length < 2) {
        return { success: false, message: 'Search query must be at least 2 characters' };
      }

      const response = await friendsApi.searchUsers(query.trim(), page, limit);
      
      if (response.success) {
        const formattedUsers = response.data.map(user => ({
          id: user.userId,
          name: `${user.firstName} ${user.lastName}`.trim(),
          avatar: user.profilePicture || '/default-avatar.png',
          mutualFriends: user.mutualFriendsCount || 0,
          isOnline: user.isOnline || false,
          friendshipStatus: user.friendshipStatus || 'none',
          canSendRequest: user.friendshipStatus === 'none'
        }));

        return {
          success: true,
          data: formattedUsers,
          pagination: response.pagination,
          message: `Found ${formattedUsers.length} users`
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to search users');
    }
  },

  /**
   * Load user's friends with pagination
   */
  loadFriends: async (userId, page = 1, limit = 50) => {
    try {
      const response = await friendsApi.loadFriends(userId, page, limit);
      
      if (response.success) {
        const formattedFriends = response.data.map(friend => ({
          id: friend.userId,
          friendshipId: friend.friendshipId,
          name: `${friend.firstName} ${friend.lastName}`.trim(),
          avatar: friend.profilePicture || '/default-avatar.png',
          mutualFriends: friend.mutualFriendsCount || 0,
          isOnline: friend.isOnline || false,
          lastActive: friend.lastActiveAt,
          friendsSince: friend.acceptedAt,
          status: 'accepted'
        }));

        return {
          success: true,
          data: formattedFriends,
          pagination: response.pagination,
          message: `Loaded ${formattedFriends.length} friends`
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to load friends');
    }
  },

  /**
   * Load received friend requests
   */
  loadReceivedRequests: async (userId, page = 1, limit = 20) => {
    try {
      const response = await friendsApi.loadReceivedRequests(userId, page, limit);
      
      if (response.success) {
        const formattedRequests = response.data.map(request => ({
          id: request.friendshipId,
          requesterId: request.requesterId,
          name: `${request.requesterFirstName} ${request.requesterLastName}`.trim(),
          avatar: request.requesterAvatar || '/default-avatar.png',
          mutualFriends: request.mutualFriendsCount || 0,
          requestedAt: request.createdAt,
          message: request.message || null,
          type: 'received'
        }));

        return {
          success: true,
          data: formattedRequests,
          pagination: response.pagination,
          message: `${formattedRequests.length} pending requests`
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to load friend requests');
    }
  },

  /**
   * Load sent friend requests
   */
  loadSentRequests: async (userId, page = 1, limit = 20) => {
    try {
      const response = await friendsApi.loadSentRequests(userId, page, limit);
      if (response.success) {
        const formattedRequests = response.data.map(request => ({
          id: request.friendshipId,
          recipientId: request.recipientId,
          name: `${request.recipientFirstName} ${request.recipientLastName}`.trim(),
          avatar: request.recipientAvatar || '/default-avatar.png',
          mutualFriends: request.mutualFriendsCount || 0,
          sentAt: request.createdAt,
          type: 'sent'
        }));

        return {
          success: true,
          data: formattedRequests,
          pagination: response.pagination,
          message: `${formattedRequests.length} sent requests`
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to load sent requests');
    }
  },

  /**
   * Send friend request
   */
  sendFriendRequest: async (recipientId, message = null) => {
    try {
      const currentUserId = getCurrentUserId();
      
      if (currentUserId === recipientId) {
        return { success: false, message: "You can't send a friend request to yourself" };
      }

      const response = await friendsApi.sendFriendRequest(currentUserId, recipientId);
      
      if (response.success) {
        showNotification('Friend request sent successfully', 'success');
        return {
          success: true,
          data: response.data,
          message: 'Friend request sent successfully'
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to send friend request');
    }
  },

  /**
   * Accept friend request
   */
  acceptFriendRequest: async (friendshipId) => {
    try {
      const currentUserId = getCurrentUserId();
      const response = await friendsApi.acceptFriendRequest(friendshipId, currentUserId);
      
      if (response.success) {
        showNotification('Friend request accepted', 'success');
        return {
          success: true,
          data: response.data,
          message: 'Friend request accepted successfully'
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to accept friend request');
    }
  },

  /**
   * Decline friend request
   */
  declineFriendRequest: async (friendshipId) => {
    try {
      const currentUserId = getCurrentUserId();
      const response = await friendsApi.declineFriendRequest(friendshipId, currentUserId);
      
      if (response.success) {
        showNotification('Friend request declined', 'info');
        return {
          success: true,
          data: response.data,
          message: 'Friend request declined'
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to decline friend request');
    }
  },

  /**
   * Cancel sent friend request
   */
  cancelFriendRequest: async (friendshipId) => {
    try {
      const currentUserId = getCurrentUserId();
      const response = await friendsApi.cancelFriendRequest(friendshipId, currentUserId);
      
      if (response.success) {
        showNotification('Friend request cancelled', 'info');
        return {
          success: true,
          data: response.data,
          message: 'Friend request cancelled'
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to cancel friend request');
    }
  },

  /**
   * Remove friend
   */
  removeFriend: async (friendshipId, friendName = 'Friend') => {
    try {
      const currentUserId = getCurrentUserId();
      const response = await friendsApi.removeFriend(friendshipId, currentUserId);
      
      if (response.success) {
        showNotification(`${friendName} removed from friends`, 'info');
        return {
          success: true,
          data: response.data,
          message: 'Friend removed successfully'
        };
      }
      
      return response;
    } catch (error) {
      return handleApiError(error, 'Failed to remove friend');
    }
  }
};