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
      ["All user IDs must be valid, non-empty, and positive numbers"]
    );
  }
  return null;
};

/**
 * Validates pagination parameters
 */
export const validatePagination = (page, limit) => {
  const errors = [];
  
  if (!Number.isInteger(page) || page < 1) {
    errors.push("Page must be a positive integer >= 1");
  }
  
  if (!Number.isInteger(limit) || limit < 1 || limit > 100) {
    errors.push("Limit must be an integer between 1 and 100");
  }
  
  if (errors.length > 0) {
    return failResponse("Invalid pagination parameters", errors);
  }
  
  return null;
};

/**
 * Validates search filter
 */
export const validateSearchFilter = (filter) => {
  if (typeof filter !== 'string') {
    return failResponse(
      "Invalid search filter",
      ["Filter must be a string"]
    );
  }
  
  if (filter.length > 100) {
    return failResponse(
      "Invalid search filter",
      ["Filter must not exceed 100 characters"]
    );
  }
  
  return null;
};

/**
 * Validates that two user IDs are different
 */
export const validateDifferentUsers = (userId1, userId2) => {
  if (userId1 === userId2) {
    return failResponse(
      "Invalid operation",
      ["Cannot perform this action with the same user"]
    );
  }
  return null;
};