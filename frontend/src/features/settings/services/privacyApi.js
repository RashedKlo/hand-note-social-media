import { apiClient } from "../../../services/api/client";

export const privacyApi = {
  /**
   * Reset user privacy settings to defaults
   */
  resetPrivacySettings: async (userId) => {
    const response = await apiClient.post(`/settings/${userId}/reset`);
    return response.data;
  },

  /**
   * Update user privacy settings
   */
  updatePrivacySettings: async (userId, settings) => {
    const response = await apiClient.put(`/settings/${userId}/update`, {
      defaultPostAudience: settings.defaultPostAudience || null,
      profileVisibility: settings.profileVisibility || null,
      friendListVisibility: settings.friendListVisibility || null,
      friendRequestFrom: settings.friendRequestFrom || null,
      messageFrom: settings.messageFrom || null,
    });
    return response.data;
  },
};