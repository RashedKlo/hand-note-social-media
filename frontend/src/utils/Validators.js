import { failResponse, successResponse } from "../services/api/apiHandler";


/**
 * Validates an ID (positive integer)
 * @param {number} id - The ID to validate
 * @param {string} fieldName - Name of the field for error messages (e.g., "User ID", "Post ID")
 * @returns {Object} Response object with success flag
 */
export const validateId = (id, fieldName = "ID") => {
  if (!id) {
    return failResponse(
      `Invalid ${fieldName}`,
      [`${fieldName} is required`]
    );
  }

  if (typeof id !== 'number' || !Number.isInteger(id) || id < 1) {
    return failResponse(
      `Invalid ${fieldName}`,
      [`${fieldName} must be a positive integer`]
    );
  }

  return successResponse(id, `${fieldName} is valid`);
};

/**
 * Validates pagination parameters
 * @param {number} page - Page number (must be >= 1)
 * @param {number} limit - Items per page
 * @param {Object} options - Validation options
 * @param {number} options.minLimit - Minimum allowed limit (default: 1)
 * @param {number} options.maxLimit - Maximum allowed limit (default: 100)
 * @returns {Object} Response object with success flag
 */
export const validatePagination = (page, limit, options = {}) => {
  const { minLimit = 1, maxLimit = 100 } = options;

  if (!Number.isInteger(page) || page < 1) {
    return failResponse(
      "Invalid pagination",
      ["Page must be a positive integer"]
    );
  }
   
  if (!Number.isInteger(limit) || limit < minLimit || limit > maxLimit) {
    return failResponse(
      "Invalid pagination",
      [`Limit must be an integer between ${minLimit} and ${maxLimit}`]
    );
  }

  return successResponse({ page, limit }, "Pagination parameters are valid");
};

/**
 * Validates a string field with optional constraints
 * @param {string} value - The string value to validate
 * @param {string} fieldName - Name of the field for error messages
 * @param {Object} options - Validation options
 * @param {number} options.minLength - Minimum length
 * @param {number} options.maxLength - Maximum length
 * @param {RegExp} options.pattern - Regex pattern to match
 * @param {string} options.patternMessage - Custom error message for pattern mismatch
 * @param {boolean} options.required - Whether the field is required (default: true)
 * @returns {Object} Response object with success flag
 */
export const validateString = (value, fieldName, options = {}) => {
  const {
    minLength = null,
    maxLength = null,
    pattern = null,
    patternMessage = null,
    required = true
  } = options;

  // Check if required
  if (required && (!value || typeof value !== 'string' || value.trim() === '')) {
    return failResponse(
      `Invalid ${fieldName}`,
      [`${fieldName} is required and must be a non-empty string`]
    );
  }

  // Skip further validation if optional and empty
  if (!required && (!value || value.trim() === '')) {
    return successResponse(value || '', `${fieldName} is valid (optional)`);
  }

  // Check minimum length
  if (minLength && value.length < minLength) {
    return failResponse(
      `Invalid ${fieldName}`,
      [`${fieldName} must be at least ${minLength} characters long`]
    );
  }

  // Check maximum length
  if (maxLength && value.length > maxLength) {
    return failResponse(
      `Invalid ${fieldName}`,
      [`${fieldName} must not exceed ${maxLength} characters`]
    );
  }

  // Check pattern
  if (pattern && !pattern.test(value)) {
    return failResponse(
      `Invalid ${fieldName}`,
      [patternMessage || `${fieldName} format is invalid`]
    );
  }

  return successResponse(value, `${fieldName} is valid`);
};