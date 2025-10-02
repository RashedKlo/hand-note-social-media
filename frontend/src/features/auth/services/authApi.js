import { apiClient } from "../../../services/api/client";

export const authApi = {
  /**
   * Register a new user
   * @param {Object} userData - User registration data
   * @returns {Promise} Response with user authentication data
   */
  registerUser: async (userData) => {
    const response = await apiClient.post("/auth/register", userData);
    return response.data;
  },

  /**
   * Login existing user
   * @param {Object} userData - User login credentials
   * @returns {Promise} Response with user authentication data
   */
  loginUser: async (userData) => {
    const response = await apiClient.post("/auth/login", userData);
    return response.data;
  },

  /**
   * Initiate Google OAuth login
   * Redirects user to Google login page
   * @returns {string} Google OAuth URL
   */
  loginWithGoogle: () => {
    // Redirect to backend Google OAuth endpoint
    window.location.href = `${apiClient.defaults.baseURL}/auth/sign-google`;
  },

  /**
   * Alternative: Get Google OAuth URL without redirect
   * @returns {string} Google OAuth URL
   */
  getGoogleLoginUrl: () => {
    return `${apiClient.defaults.baseURL}/auth/sign-google`;
  }
};

