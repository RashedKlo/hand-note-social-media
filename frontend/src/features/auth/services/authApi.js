import { apiClient } from "../../../services/api/client";

export const authApi = {
  /**
   * Register a new user
   */
registerUser: async (registrationData) => {
    const response = await apiClient.post('/auth/register', {
      firstName: registrationData.firstName,
      lastName: registrationData.lastName,
      cityId: registrationData.cityId,
      userName: registrationData.userName,
      email: registrationData.email,
      password: registrationData.password,
      confirmPassword: registrationData.confirmPassword,
    });
    return response.data;
  },

  /**
   * Login user
   */
  loginUser: async (loginData) => {
    const response = await apiClient.post('/auth/login', {
      email: loginData.email,
      password: loginData.password,
    });
    return response.data;
  },

  /**
   * Get Google OAuth login URL
   */
  getGoogleLoginUrl: () => {
    // Assuming your API base URL is configured in apiClient
    const baseUrl = apiClient.defaults.baseURL || '';
    return `${baseUrl}/auth/sign-google`;
  },
};

