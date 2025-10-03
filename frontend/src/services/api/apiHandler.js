export const successResponse = (data, message = null) => ({
  success: true,
  data,
  message
});

/**
 * Creates a failure response object
 */
export const failResponse = (message, errors = []) => ({
  success: false,
  message,
  errors
});
  
/**
 * Handles API errors and formats them consistently
 */
export const handleError = async (error) => {
  console.log("Full error:", error);
  console.log("Error response:", error.response);
  console.log("Error response data:", error.response?.data);

  const apiError = error.response?.data;
  if (apiError) {
    return failResponse(
      apiError.Message || "An error occurred",
      apiError.Errors || []
    );
  }
  return failResponse(
    "Network error occurred",
    ["Unable to connect to server"]
  );
};

/**
 * Generic API handler with validation support
 * @param {Function} action - Function that returns a Promise (API call)
 * @param {Function|null} validator - Optional validation function
 */  

export const handleApi = async (action, validator = null) => {
  try {
    // Run validation if provided
    if (validator) {
      const validationError = validator();
      if (validationError) {
        return validationError;
      }
    }
    
    // Execute the API call
    const response = await action();
    return successResponse(response.Data, response.Message);
  } catch (error) {
    return handleError(error);
  }
};