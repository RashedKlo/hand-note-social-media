import { apiClient } from "../../../services/api/client";

export const messagesApi = {
  /**
   * Send a message
   */
  sendMessage: async (messageData) => {
    const response = await apiClient.post('/messages', {
      conversationId: messageData.conversationId,
      senderId: messageData.senderId,
      content: messageData.content,
      messageType: messageData.messageType || 'text',
      mediaId: messageData.mediaId || null,
    });
    return response.data;
  },

  /**
   * Mark messages as read
   */
  markAsRead: async (conversationId, userId) => {
    const response = await apiClient.put(`/messages/${conversationId}/read`, {
      userId,
    });
    return response.data;
  },

  /**
   * Get messages
   */
  getMessages: async (conversationId, currentUserId, pageSize = 20, pageNumber = 1) => {
    const response = await apiClient.get(`/messages/${conversationId}/search`, {
      data: {
        currentUserId,
      },
      params: {
        pageSize,
        pageNumber,
      },
    });
    return response.data;
  },
};
