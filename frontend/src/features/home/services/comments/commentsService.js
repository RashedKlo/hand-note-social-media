import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateCreateCommentData,
  validateUpdateCommentData,
  validateDeleteCommentData,
  validateCommentId,
  validateCommentReactionData,
  validateDeleteCommentReactionData
} from "../utils/commentsValidation";
import { commentsApi } from "./commentsApi";

export const commentsService = {
  /**
   * Create a new comment
   */
  createComment: async (commentData) =>
    handleApi(
      () => commentsApi.createComment(commentData),
      () => validateCreateCommentData(commentData)
    ),

  /**
   * Update a comment
   */
  updateComment: async (commentId, commentData) =>
    handleApi(
      () => commentsApi.updateComment(commentId, commentData),
      () => validateUpdateCommentData(commentId, commentData)
    ),

  /**
   * Delete a comment
   */
  deleteComment: async (commentId, userId) =>
    handleApi(
      () => commentsApi.deleteComment(commentId, userId),
      () => validateDeleteCommentData(commentId, userId)
    ),

  /**
   * Check if comment exists
   */
  checkCommentExists: async (commentId) =>
    handleApi(
      () => commentsApi.checkCommentExists(commentId),
      () => validateCommentId(commentId)
    ),

  /**
   * Create comment reaction
   */
  createCommentReaction: async (reactionData) =>
    handleApi(
      () => commentsApi.createCommentReaction(reactionData),
      () => validateCommentReactionData(reactionData)
    ),

  /**
   * Delete comment reaction
   */
  deleteCommentReaction: async (commentId, userId) =>
    handleApi(
      () => commentsApi.deleteCommentReaction(commentId, userId),
      () => validateDeleteCommentReactionData(commentId, userId)
    ),
};