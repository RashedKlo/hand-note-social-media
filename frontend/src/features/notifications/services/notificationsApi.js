import { apiClient } from "../../../services/api/client";

export const notificationsApi = {
  /**
   * Create a new notification
   */
  createNotification: async (notificationData) => {
    const response = await apiClient.post('/notifications', notificationData);
    return response.data;
  },

  /**
   * Get user notifications with pagination
   */
  getUserNotifications: async (userId, pageSize = 20, pageNumber = 1, includeRead = true) => {
    const response = await apiClient.get(`/notifications/users/${userId}`, {
      params: {
        pageSize,
        pageNumber,
        includeRead
      }
    });
    return response.data;
  },

  /**
   * Mark notification as read
   */
  markAsRead: async (notificationId, userId) => {
    const response = await apiClient.put(`/notifications/${notificationId}/read`, {
      userId
    });
    return response.data;
  },

  /**
   * Get unread notification count
   */
  getUnreadCount: async (userId) => {
    const response = await apiClient.get(`/notifications/${userId}/unread-count`);
    return response.data;
  }
};