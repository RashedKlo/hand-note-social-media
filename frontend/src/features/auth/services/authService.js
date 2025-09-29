import { authApi } from "./authApi";

export const authService = {
 registerUser: async (userData) => {
    try {
      const response = await authApi.registerUser(userData);
      
      // Response structure: { Success: true, Data: UserAuthenticationData, Message: string }
      return {
        success: response.Success,
        data: response.Data,
        message: response.Message
      };
      
    } catch (error) {
      console.log('Full error:', error);
      console.log('Error response:', error.response);
      console.log('Error response data:', error.response?.data);
      
      // Check if it's an API error (400, 401, 500, etc.) from your backend
      const apiError = error.response?.data;
      
      if (apiError) {
        // This contains your C# ErrorResponse: { Success: false, Message: string, Errors: string[] }
        console.log('API Error from backend:', apiError);
        return {
          success: apiError.Success,           // false
          message: apiError.Message,           // "Invalid credentials" 
          errors: apiError.Errors || []        // ["Email not found"]
        };
      }
      
      // Network errors (server down, no internet, etc.) - no response from server
      console.log('Network error:', error.message);
      return {
        success: false,
        message: error.message || 'Network error occurred',
        errors: ['Unable to connect to server']
      };
    }
  },

  loginUser: async (userData) => {
    try {
      const response = await authApi.loginUser(userData);
      
      // Response structure: { Success: true, Data: UserAuthenticationData, Message: string }
      return {
        success: response.Success,
        data: response.Data,
        message: response.Message
      };
      
    } catch (error) {
      console.log('Full error:', error);
      console.log('Error response:', error.response);
      console.log('Error response data:', error.response?.data);
      
      // Check if it's an API error (400, 401, 500, etc.) from your backend
      const apiError = error.response?.data;
      
      if (apiError) {
        // This contains your C# ErrorResponse: { Success: false, Message: string, Errors: string[] }
        console.log('API Error from backend:', apiError);
        return {
          success: apiError.Success,           // false
          message: apiError.Message,           // "Invalid credentials" 
          errors: apiError.Errors || []        // ["Email not found"]
        };
      }
      
      // Network errors (server down, no internet, etc.) - no response from server
      console.log('Network error:', error.message);
      return {
        success: false,
        message: error.message || 'Network error occurred',
        errors: ['Unable to connect to server']
      };
    }
  }
};
