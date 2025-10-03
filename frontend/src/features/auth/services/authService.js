import { handleApi } from "../../../services/api/apiHandler";
import { validateLoginData } from "../utils/loginValidation";
import { validateRegistrationData } from "../utils/registerValidation";

import { authApi } from "./authApi";

export const authService = {
  /**
   * Register a new user
   */
  registerUser: async (registrationData) =>
    handleApi(
      () => authApi.registerUser(registrationData),
      () => validateRegistrationData(registrationData)
    ),

  /**
   * Login user
   */
  loginUser: async (loginData) =>
    handleApi(
      () => authApi.loginUser(loginData),
      () => validateLoginData(loginData)
    ),

  /**
   * Get Google OAuth login URL
   */
  getGoogleLoginUrl: () => authApi.getGoogleLoginUrl(),
};



