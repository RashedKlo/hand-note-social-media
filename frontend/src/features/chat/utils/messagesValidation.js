import { failResponse, successResponse } from "../../../services/api/apiHandler";
import { validateId, validateString, validatePagination } from "../../../utils/Helpers";

/**
 * Valid message types
 */
const VALID_MESSAGE_TYPES = ['text', 'image', 'file', 'system'];

/**
 * Validates message type
 */
const validateMessageType = (messageType) => {
  if (messageType && !VALID_MESSAGE_TYPES.includes(messageType)) {
    return failResponse(
      "Invalid message type",
      ["MessageType must be one of: text, image, file, system"]
    );
  }
  return successResponse(messageType, "Message type is valid");
};

/**
 * Validates send message request
 */
export const validateSendMessage = (messageData) => {
  // Validate conversationId
  const conversationResult = validateId(messageData?.conversationId, "Conversation ID");
  if (!conversationResult.success) {
    return conversationResult;
  }

  // Validate senderId
  const senderResult = validateId(messageData?.senderId, "Sender ID");
  if (!senderResult.success) {
    return senderResult;
  }

  // Validate content
  const contentResult = validateString(messageData?.content, "Content", {
    minLength: 1,
    maxLength: 2000
  });
  if (!contentResult.success) {
    return contentResult;
  }

  // Validate messageType (optional)
  if (messageData?.messageType) {
    const typeResult = validateMessageType(messageData.messageType);
    if (!typeResult.success) {
      return typeResult;
    }
  }

  // Validate mediaId (optional)
  if (messageData?.mediaId) {
    const mediaResult = validateId(messageData.mediaId, "Media ID");
    if (!mediaResult.success) {
      return mediaResult;
    }
  }

  return successResponse(messageData, "Message data is valid");
};

/**
 * Validates mark as read request
 */
export const validateMarkAsRead = (conversationId, userId) => {
  // Validate conversationId
  const conversationResult = validateId(conversationId, "Conversation ID");
  if (!conversationResult.success) {
    return conversationResult;
  }

  // Validate userId
  const userResult = validateId(userId, "User ID");
  if (!userResult.success) {
    return userResult;
  }

  return successResponse({ conversationId, userId }, "Mark as read data is valid");
};

/**
 * Validates get messages request
 */
export const validateGetMessages = (conversationId, currentUserId, options = {}) => {
  // Validate conversationId
  const conversationResult = validateId(conversationId, "Conversation ID");
  if (!conversationResult.success) {
    return conversationResult;
  }

  // Validate currentUserId
  const userResult = validateId(currentUserId, "Current User ID");
  if (!userResult.success) {
    return userResult;
  }

  // Validate pagination
  const pageSize = options.pageSize || 20;
  const pageNumber = options.pageNumber || 1;
  
  const paginationResult = validatePagination(pageNumber, pageSize, {
    minLimit: 1,
    maxLimit: 100
  });
  if (!paginationResult.success) {
    return paginationResult;
  }

  return successResponse({ conversationId, currentUserId, pageSize, pageNumber }, "Get messages data is valid");
};
