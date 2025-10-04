import { apiClient } from "../../../services/api/client";

export const postsApi = {
  /**
   * Create a new post
   */
  createPost: async (postData) => {
    const response = await apiClient.post('/posts', {
      userId: postData.userId,
      content: postData.content || null,
      postType: postData.postType || 'text',
      sharedPostId: postData.sharedPostId || null,
      hasMedia: postData.hasMedia || false,
      allowsComments: postData.allowsComments ?? true,
      allowsShares: postData.allowsShares ?? true,
      mediaFilesID: postData.mediaFilesID || null,
    });
    return response.data;
  },

  /**
   * Delete a post
   */
  deletePost: async (postId, userId) => {
    const response = await apiClient.delete(`/posts/${postId}`, {
      data: {
        userId: userId,
      },
    });
    return response.data;
  },

  /**
   * Check if post exists
   */
  checkPostExists: async (postId) => {
    const response = await apiClient.get(`/posts/${postId}/exists`);
    return response.data;
  },

  /**
   * Create post reaction
   */
  createPostReaction: async (reactionData) => {
    const response = await apiClient.post('/posts/reactions', {
      postId: reactionData.postId,
      userId: reactionData.userId,
      reactionType: reactionData.reactionType,
    });
    return response.data;
  },

  /**
   * Delete post reaction
   */
  deletePostReaction: async (postId, userId) => {
    const response = await apiClient.delete(`/posts/${postId}/reactions`, {
      data: {
        userId: userId,
      },
    });
    return response.data;
  },
};