import { failResponse } from "../../../services/api/apiHandler";

/**
 * Validates that all provided IDs are valid and non-empty
 */
export const validateIds = (...ids) => {
  const invalidIds = ids.filter(id => 
    !id || 
    (typeof id === 'string' && id.trim() === '') ||
    (typeof id === 'number' && (!Number.isFinite(id) || id <= 0))
  );
  
  if (invalidIds.length > 0) {
    return failResponse(
      "Invalid ID(s) provided",
      ["All IDs must be valid, non-empty, and positive numbers"]
    );
  }
  return null;
};

/**
 * Validates pagination parameters for notifications
 */
export const validatePagination = (pageSize, pageNumber) => {
  const errors = [];
  
  if (!Number.isInteger(pageSize) || pageSize < 1 || pageSize > 100) {
    errors.push("Page size must be an integer between 1 and 100");
  }
  
  if (!Number.isInteger(pageNumber) || pageNumber < 1) {
    errors.push("Page number must be a positive integer >= 1");
  }
  
  if (errors.length > 0) {
    return failResponse("Invalid pagination parameters", errors);
  }
  
  return null;
};

/**
 * Validates notification creation data
 */
export const validateNotificationData = (data) => {
  const errors = [];

  // Required fields
  if (!data.recipientId || data.recipientId <= 0) {
    errors.push("RecipientId is required and must be a positive integer");
  }

  if (!data.notificationType || typeof data.notificationType !== 'string') {
    errors.push("NotificationType is required and must be a string");
  } else if (data.notificationType.length < 1 || data.notificationType.length > 20) {
    errors.push("NotificationType must be between 1 and 20 characters");
  }

  if (!data.title || typeof data.title !== 'string') {
    errors.push("Title is required and must be a string");
  } else if (data.title.length < 3 || data.title.length > 50) {
    errors.push("Title must be between 3 and 50 characters");
  }

  // Optional fields validation
  if (data.actorId !== undefined && data.actorId !== null && data.actorId <= 0) {
    errors.push("ActorId must be a positive integer if provided");
  }

  if (data.targetType && typeof data.targetType === 'string') {
    const validTargetTypes = ['post', 'comment', 'user', 'friendship'];
    if (!validTargetTypes.includes(data.targetType)) {
      errors.push("TargetType must be one of: post, comment, user, friendship");
    }
    if (data.targetType.length > 20) {
      errors.push("TargetType cannot exceed 20 characters");
    }
  }

  if (data.targetId !== undefined && data.targetId !== null && data.targetId <= 0) {
    errors.push("TargetId must be a positive integer if provided");
  }

  if (data.message && typeof data.message === 'string' && data.message.length > 200) {
    errors.push("Message cannot exceed 200 characters");
  }

  if (data.expiresAt && !(data.expiresAt instanceof Date) && isNaN(Date.parse(data.expiresAt))) {
    errors.push("ExpiresAt must be a valid date");
  }

  if (errors.length > 0) {
    return failResponse("Invalid notification data", errors);
  }

  return null;
};

/**
 * Validates includeRead parameter
 */
export const validateIncludeRead = (includeRead) => {
  if (typeof includeRead !== 'boolean') {
    return failResponse(
      "Invalid includeRead parameter",
      ["includeRead must be a boolean value"]
    );
  }
  return null;
};