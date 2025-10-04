import { failResponse } from "../../../services/api/apiHandler";

/**
 * Valid post types
 */
const VALID_POST_TYPES = ['text', 'media', 'shared'];

export const validateContent = (content) => {
  if (content !== null && content !== undefined) {
    if (typeof content !== 'string') {
      return failResponse(
        "Invalid content",
        ["Content must be a string"]
      );
    }

    if (content.length > 1200) {
      return failResponse(
        "Invalid content",
        ["Content cannot exceed 1200 characters"]
      );
    }
  }

  return null;
};

/**
 * Validates post type
 */
export const validatePostType = (postType) => {
  if (!postType || typeof postType !== 'string') {
    return failResponse(
      "Invalid post type",
      ["PostType is required"]
    );
  }

  if (!VALID_POST_TYPES.includes(postType)) {
    return failResponse(
      "Invalid post type",
      ["PostType must be: text, media, or shared"]
    );
  }

  return null;
};

/**
 * Validates shared post ID (optional)
 */
export const validateSharedPostId = (sharedPostId) => {
  if (sharedPostId !== null && sharedPostId !== undefined) {
    if (typeof sharedPostId !== 'number' || !Number.isInteger(sharedPostId)) {
      return failResponse(
        "Invalid shared post ID",
        ["SharedPostId must be a positive integer"]
      );
    }

    if (sharedPostId < 1) {
      return failResponse(
        "Invalid shared post ID",
        ["SharedPostId must be a positive integer"]
      );
    }
  }

  return null;
};

/**
 * Validates media files IDs (optional array)
 */
export const validateMediaFilesID = (mediaFilesID) => {
  if (mediaFilesID !== null && mediaFilesID !== undefined) {
    if (!Array.isArray(mediaFilesID)) {
      return failResponse(
        "Invalid media files",
        ["MediaFilesID must be an array"]
      );
    }

    for (const id of mediaFilesID) {
      if (typeof id !== 'number' || !Number.isInteger(id) || id < 1) {
        return failResponse(
          "Invalid media files",
          ["All MediaFilesID must be positive integers"]
        );
      }
    }
  }

  return null;
};

/**
 * Validates reaction type
 */
export const validateReactionType = (reactionType) => {
  if (!reactionType || typeof reactionType !== 'string') {
    return failResponse(
      "Invalid reaction type",
      ["ReactionType is required"]
    );
  }

  if (reactionType.length > 10) {
    return failResponse(
      "Invalid reaction type",
      ["ReactionType cannot exceed 10 characters"]
    );
  }

  if (!VALID_REACTION_TYPES.includes(reactionType)) {
    return failResponse(
      "Invalid reaction type",
      ["ReactionType must be one of: like, love, haha, wow, sad, angry, care"]
    );
  }

  return null;
};

/**
 * Validates create post data
 */
export const validateCreatePostData = (postData) => {
  const errors = [];

  // Validate userId
  const userIdError = validateUserId(postData?.userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  // Validate content (optional)
  const contentError = validateContent(postData?.content);
  if (contentError) {
    errors.push(...contentError.errors);
  }

  // Validate postType
  const postTypeError = validatePostType(postData?.postType);
  if (postTypeError) {
    errors.push(...postTypeError.errors);
  }

  // Validate sharedPostId (optional)
  const sharedPostIdError = validateSharedPostId(postData?.sharedPostId);
  if (sharedPostIdError) {
    errors.push(...sharedPostIdError.errors);
  }

  // Validate mediaFilesID (optional)
  const mediaFilesError = validateMediaFilesID(postData?.mediaFilesID);
  if (mediaFilesError) {
    errors.push(...mediaFilesError.errors);
  }

  // Validate boolean fields
  if (postData?.hasMedia !== undefined && typeof postData.hasMedia !== 'boolean') {
    errors.push("HasMedia must be a boolean");
  }

  if (postData?.allowsComments !== undefined && typeof postData.allowsComments !== 'boolean') {
    errors.push("AllowsComments must be a boolean");
  }

  if (postData?.allowsShares !== undefined && typeof postData.allowsShares !== 'boolean') {
    errors.push("AllowsShares must be a boolean");
  }

  if (errors.length > 0) {
    return failResponse("Invalid post data", errors);
  }

  return null;
};

/**
 * Validates delete post data
 */
export const validateDeletePostData = (postId, userId) => {
  const errors = [];

  // Validate postId
  const postIdError = validatePostId(postId);
  if (postIdError) {
    errors.push(...postIdError.errors);
  }

  // Validate userId
  const userIdError = validateUserId(userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid delete data", errors);
  }

  return null;
};

/**
 * Validates reaction data
 */
export const validateReactionData = (reactionData) => {
  const errors = [];

  // Validate postId
  const postIdError = validatePostId(reactionData?.postId);
  if (postIdError) {
    errors.push(...postIdError.errors);
  }

  // Validate userId
  const userIdError = validateUserId(reactionData?.userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  // Validate reactionType
  const reactionTypeError = validateReactionType(reactionData?.reactionType);
  if (reactionTypeError) {
    errors.push(...reactionTypeError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid reaction data", errors);
  }

  return null;
};

/**
 * Validates delete reaction data
 */
export const validateDeleteReactionData = (postId, userId) => {
  const errors = [];

  // Validate postId
  const postIdError = validatePostId(postId);
  if (postIdError) {
    errors.push(...postIdError.errors);
  }

  // Validate userId
  const userIdError = validateUserId(userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid delete reaction data", errors);
  }

  return null;
};
