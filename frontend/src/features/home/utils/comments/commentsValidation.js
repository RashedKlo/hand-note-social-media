import { validateID } from "../../../../utils/Helpers";
import { failResponse } from "../../../services/api/apiHandler";



/**
 * Validates parent comment ID (optional)
 */
export const validateParentCommentId = (parentCommentId) => {
  if (parentCommentId !== null && parentCommentId !== undefined) {
    if (typeof parentCommentId !== 'number' || !Number.isInteger(parentCommentId)) {
      return failResponse(
        "Invalid parent comment ID",
        ["ParentCommentId must be a positive integer"]
      );
    }

    if (parentCommentId < 1) {
      return failResponse(
        "Invalid parent comment ID",
        ["ParentCommentId must be a positive integer"]
      );
    }
  }

  return null;
};

/**
 * Validates comment content
 */
export const validateContent = (content) => {
  if (!content || typeof content !== 'string' || content.trim() === '') {
    return failResponse(
      "Invalid content",
      ["Content is required and must be a non-empty string"]
    );
  }

  if (content.length < 1) {
    return failResponse(
      "Invalid content",
      ["Content must be at least 1 character long"]
    );
  }

  if (content.length > 200) {
    return failResponse(
      "Invalid content",
      ["Content must not exceed 200 characters"]
    );
  }

  return null;
};

/**
 * Validates media ID (optional)
 */
export const validateMediaId = (mediaId) => {
  if (mediaId !== null && mediaId !== undefined) {
    if (typeof mediaId !== 'number' || !Number.isInteger(mediaId)) {
      return failResponse(
        "Invalid media ID",
        ["MediaId must be a positive integer"]
      );
    }

    if (mediaId < 1) {
      return failResponse(
        "Invalid media ID",
        ["MediaId must be a positive integer"]
      );
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
 * Validates create comment data
 */
export const validateCreateCommentData = (commentData) => {
  const errors = [];

  // Validate postId
  const postIdError = validatePostId(commentData?.postId);
  if (postIdError) {
    errors.push(...postIdError.errors);
  }

  // Validate userId
  const userIdError = validateUserId(commentData?.userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  // Validate parentCommentId (optional)
  const parentCommentIdError = validateParentCommentId(commentData?.parentCommentId);
  if (parentCommentIdError) {
    errors.push(...parentCommentIdError.errors);
  }

  // Validate content
  const contentError = validateContent(commentData?.content);
  if (contentError) {
    errors.push(...contentError.errors);
  }

  // Validate mediaId (optional)
  const mediaIdError = validateMediaId(commentData?.mediaId);
  if (mediaIdError) {
    errors.push(...mediaIdError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid comment data", errors);
  }

  return null;
};

/**
 * Validates update comment data
 */
export const validateUpdateCommentData = (commentId, commentData) => {
  const errors = [];

  // Validate commentId
  const commentIdError = validateCommentId(commentId);
  if (commentIdError) {
    errors.push(...commentIdError.errors);
  }

  // Validate userId
  const userIdError = validateUserId(commentData?.userId);
  if (userIdError) {
    errors.push(...userIdError.errors);
  }

  // Validate content
  const contentError = validateContent(commentData?.content);
  if (contentError) {
    errors.push(...contentError.errors);
  }

  // Validate mediaId (optional)
  const mediaIdError = validateMediaId(commentData?.mediaId);
  if (mediaIdError) {
    errors.push(...mediaIdError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid update data", errors);
  }

  return null;
};

/**
 * Validates delete comment data
 */
export const validateDeleteCommentData = (commentId, userId) => {
  const errors = [];

  // Validate commentId
  const commentIdError = validateCommentId(commentId);
  if (commentIdError) {
    errors.push(...commentIdError.errors);
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
 * Validates comment reaction data
 */
export const validateCommentReactionData = (reactionData) => {
  const errors = [];

  // Validate commentId
  const commentIdError = validateID(reactionData?.commentId,"Invalid comment ID");
  if (commentIdError) {
    errors.push(...commentIdError.errors);
  }

  // Validate userId
  const userIdError = validateID(reactionData?.userId,"Invalid user ID");
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
 * Validates delete comment reaction data
 */
export const validateDeleteCommentReactionData = (commentId, userId) => {
  const errors = [];

  // Validate commentId
  const commentIdError = validateID(commentId,"Invalid comment ID");
  if (!commentIdError.success) {
    errors.push(...commentIdError.errors);
  }

  // Validate userId
  const userIdError = validateID(userId,"Invalid user ID");
  if (!userIdError.success) {
    errors.push(...userIdError.errors);
  }

  if (errors.length > 0) {
    return failResponse("Invalid delete reaction data", errors);
  }

  return null;
};