import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateCreatePostData,
  validateDeletePostData,
  validatePostId,
  validateReactionData,
  validateDeleteReactionData
} from "../utils/postsValidation";
import { postsApi } from "./postsApi";

export const postsService = {
  /**
   * Create a new post
   */
  createPost: async (postData) =>
    handleApi(
      () => postsApi.createPost(postData),
      () => validateCreatePostData(postData)
    ),

  /**
   * Delete a post
   */
  deletePost: async (postId, userId) =>
    handleApi(
      () => postsApi.deletePost(postId, userId),
      () => validateDeletePostData(postId, userId)
    ),

  /**
   * Check if post exists
   */
  checkPostExists: async (postId) =>
    handleApi(
      () => postsApi.checkPostExists(postId),
      () => validatePostId(postId)
    ),

  /**
   * Create post reaction
   */
  createPostReaction: async (reactionData) =>
    handleApi(
      () => postsApi.createPostReaction(reactionData),
      () => validateReactionData(reactionData)
    ),

  /**
   * Delete post reaction
   */
  deletePostReaction: async (postId, userId) =>
    handleApi(
      () => postsApi.deletePostReaction(postId, userId),
      () => validateDeleteReactionData(postId, userId)
    ),
};