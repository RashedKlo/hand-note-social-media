import { failResponse, successResponse } from "../../../services/api/apiHandler";
import { validateId } from "../../../utils/Helpers";

/**
 * Valid audience options
 */
const AUDIENCE_OPTIONS = ['public', 'friends', 'only_me'];
const FRIEND_REQUEST_OPTIONS = ['public', 'friends_of_friends'];
const MESSAGE_FROM_OPTIONS = ['public', 'friends_of_friends', 'friends'];

/**
 * Validates audience setting
 */
const validateAudienceSetting = (value, fieldName, allowedOptions) => {
  if (value !== null && value !== undefined) {
    if (typeof value !== 'string') {
      return failResponse(
        `Invalid ${fieldName}`,
        [`${fieldName} must be a string`]
      );
    }

    if (!allowedOptions.includes(value)) {
      return failResponse(
        `Invalid ${fieldName}`,
        [`${fieldName} must be one of: ${allowedOptions.join(', ')}`]
      );
    }
  }

  return successResponse(value, `${fieldName} is valid`);
};

/**
 * Validates reset privacy settings request
 */
export const validateResetPrivacySettings = (userId) => {
  const userIdResult = validateId(userId, "User ID");
  if (!userIdResult.success) {
    return userIdResult;
  }

  return successResponse(userId, "Reset request is valid");
};

/**
 * Validates update privacy settings request
 */
export const validateUpdatePrivacySettings = (userId, settings) => {
  // Validate userId
  const userIdResult = validateId(userId, "User ID");
  if (!userIdResult.success) {
    return userIdResult;
  }

  // Validate defaultPostAudience
  if (settings?.defaultPostAudience !== undefined) {
    const result = validateAudienceSetting(
      settings.defaultPostAudience,
      "Default Post Audience",
      AUDIENCE_OPTIONS
    );
    if (!result.success) return result;
  }

  // Validate profileVisibility
  if (settings?.profileVisibility !== undefined) {
    const result = validateAudienceSetting(
      settings.profileVisibility,
      "Profile Visibility",
      AUDIENCE_OPTIONS
    );
    if (!result.success) return result;
  }

  // Validate friendListVisibility
  if (settings?.friendListVisibility !== undefined) {
    const result = validateAudienceSetting(
      settings.friendListVisibility,
      "Friend List Visibility",
      AUDIENCE_OPTIONS
    );
    if (!result.success) return result;
  }

  // Validate friendRequestFrom
  if (settings?.friendRequestFrom !== undefined) {
    const result = validateAudienceSetting(
      settings.friendRequestFrom,
      "Friend Request From",
      FRIEND_REQUEST_OPTIONS
    );
    if (!result.success) return result;
  }

  // Validate messageFrom
  if (settings?.messageFrom !== undefined) {
    const result = validateAudienceSetting(
      settings.messageFrom,
      "Message From",
      MESSAGE_FROM_OPTIONS
    );
    if (!result.success) return result;
  }

  return successResponse(settings, "Privacy settings are valid");
};

