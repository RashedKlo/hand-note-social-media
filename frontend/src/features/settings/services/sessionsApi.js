import { apiClient } from "../../../services/api/client";

export const sessionsApi = {
  /**
   * Logout a specific session
   */
  logoutSession: async (email, sessionId) => {
    const response = await apiClient.post('/user-sessions/logout', {
      email,
      sessionId,
    });
    return response.data;
  },

  /**
   * Logout all sessions for a user
   */
  logoutAllSessions: async (email) => {
    const response = await apiClient.post('/user-sessions/logout-all', {
      email,
    });
    return response.data;
  },

  /**
   * Get all active sessions for a user
   */
  getActiveSessions: async (userId) => {
    const response = await apiClient.get(`/user-sessions/${userId}/active`);
    return response.data;
  },
};
