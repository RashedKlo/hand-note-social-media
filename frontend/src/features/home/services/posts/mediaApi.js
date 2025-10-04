import { apiClient } from "../../../services/api/client";

export const mediaApi = {
  /**
   * Upload media files
   */
  uploadMedia: async (userId, filePaths) => {
    const response = await apiClient.post('/media', {
      userId,
      filePaths,
    });
    return response.data;
  },
};