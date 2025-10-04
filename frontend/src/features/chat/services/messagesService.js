import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateSendMessage,
  validateMarkAsRead,
  validateGetMessages
} from "../utils/messagesValidation";
import { messagesApi } from "./messagesApi";

export const messagesService = {
  /**
   * Send a message
   */
  sendMessage: async (messageData) =>
    handleApi(
      () => messagesApi.sendMessage(messageData),
      () => validateSendMessage(messageData)
    ),

  /**
   * Mark messages as read
   */
  markAsRead: async (conversationId, userId) =>
    handleApi(
      () => messagesApi.markAsRead(conversationId, userId),
      () => validateMarkAsRead(conversationId, userId)
    ),

  /**
   * Get messages
   */
  getMessages: async (conversationId, currentUserId, options = {}) =>
    handleApi(
      () => messagesApi.getMessages(
        conversationId, 
        currentUserId, 
        options.pageSize || 20, 
        options.pageNumber || 1
      ),
      () => validateGetMessages(conversationId, currentUserId, options)
    ),
};
