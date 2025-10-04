import { successResponse } from "../../../services/api/apiHandler";
import { validateId, validateString } from "../../../utils/Helpers";

const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

/**
 * Validates logout session request
 */
export const validateLogoutSession = (email, sessionId) => {
  // Validate email
  const emailResult = validateString(email, "Email", {
    maxLength: 320,
    pattern: EMAIL_REGEX,
    patternMessage: "Email must be in a valid format"
  });
  if (!emailResult.success) {
    return emailResult;
  }

  // Validate sessionId
  const sessionIdResult = validateId(sessionId, "Session ID");
  if (!sessionIdResult.success) {
    return sessionIdResult;
  }

  return successResponse({ email, sessionId }, "Logout request is valid");
};

/**
 * Validates logout all sessions request
 */
export const validateLogoutAllSessions = (email) => {
  // Validate email
  const emailResult = validateString(email, "Email", {
    maxLength: 320,
    pattern: EMAIL_REGEX,
    patternMessage: "Email must be in a valid format"
  });
  if (!emailResult.success) {
    return emailResult;
  }

  return successResponse(email, "Logout all request is valid");
};

/**
 * Validates get active sessions request
 */
export const validateGetActiveSessions = (userId) => {
  const userIdResult = validateId(userId, "User ID");
  if (!userIdResult.success) {
    return userIdResult;
  }

  return successResponse(userId, "Get sessions request is valid");
};