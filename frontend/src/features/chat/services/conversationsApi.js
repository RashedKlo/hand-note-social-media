import { apiClient } from "../../../services/api/client";

export const conversationsApi = {
  /**
   * Create a new conversation
   */
  createConversation: async (currentUserId, friendId) => {
    const response = await apiClient.post('/conversations', {
      currentUserId,
      friendId,
    });
    return response.data;
  },

  /**
   * Check if user is part of conversation (route params)
   */
  checkUserMembership: async (conversationId, userId) => {
    const response = await apiClient.get(
      `/conversations/${conversationId}/users/${userId}/membership`
    );
    return response.data;
  },

  /**
   * Check if user is part of conversation (body)
   */
  checkUserMembershipByBody: async (conversationId, userId) => {
    const response = await apiClient.post('/conversations/membership/check', {
      conversationId,
      userId,
    });
    return response.data;
  },
};
