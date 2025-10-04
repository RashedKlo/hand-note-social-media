import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateIds, 
  validatePagination, 
  validateNotificationData,
  validateIncludeRead 
} from "../utils/notificationsValidation";
import { notificationsApi } from "./notificationsApi";

export const notificationsService = {
  /**
   * Create a new notification
   */
  createNotification: async (notificationData) =>
    handleApi(
      () => notificationsApi.createNotification(notificationData),
      () => validateNotificationData(notificationData)
    ),

  /**
   * Get user notifications with pagination
   */
  getUserNotifications: async (userId, pageSize = 20, pageNumber = 1, includeRead = true) =>
    handleApi(
      () => notificationsApi.getUserNotifications(userId, pageSize, pageNumber, includeRead),
      () => validateIds(userId) || 
        validatePagination(pageSize, pageNumber) ||
        validateIncludeRead(includeRead)
    ),

  /**
   * Mark notification as read
   */
  markAsRead: async (notificationId, userId) =>
    handleApi(
      () => notificationsApi.markAsRead(notificationId, userId),
      () => validateIds(notificationId, userId)
    ),

  /**
   * Get unread notification count
   */
  getUnreadCount: async (userId) =>
    handleApi(
      () => notificationsApi.getUnreadCount(userId),
      () => validateIds(userId)
    )
};