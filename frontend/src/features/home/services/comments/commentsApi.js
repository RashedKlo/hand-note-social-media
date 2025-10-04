import { apiClient } from "../../../services/api/client";

export const commentsApi = {
  /**
   * Create a new comment
   */
  createComment: async (commentData) => {
    const response = await apiClient.post('/comments', {
      postId: commentData.postId,
      userId: commentData.userId,
      parentCommentId: commentData.parentCommentId || null,
      content: commentData.content,
      mediaId: commentData.mediaId || null,
    });
    return response.data;
  },

  /**
   * Update a comment
   */
  updateComment: async (commentId, commentData) => {
    const response = await apiClient.put(`/comments/${commentId}`, {
      userId: commentData.userId,
      content: commentData.content,
      mediaId: commentData.mediaId || null,
    });
    return response.data;
  },

  /**
   * Delete a comment
   */
  deleteComment: async (commentId, userId) => {
    const response = await apiClient.delete(`/comments/${commentId}`, {
      data: {
        userId: userId,
      },
    });
    return response.data;
  },

  /**
   * Check if comment exists
   */
  checkCommentExists: async (commentId) => {
    const response = await apiClient.get(`/comments/${commentId}/exists`);
    return response.data;
  },

  /**
   * Create comment reaction
   */
  createCommentReaction: async (reactionData) => {
    const response = await apiClient.post('/comments/reactions', {
      commentId: reactionData.commentId,
      userId: reactionData.userId,
      reactionType: reactionData.reactionType,
    });
    return response.data;
  },

  /**
   * Delete comment reaction
   */
  deleteCommentReaction: async (commentId, userId) => {
    const response = await apiClient.delete(`/comments/${commentId}/reactions`, {
      data: {
        userId: userId,
      },
    });
    return response.data;
  },
};

