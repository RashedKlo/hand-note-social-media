import { failResponse, successResponse } from "../../../services/api/apiHandler";
import { validateId, validateString } from "../../../utils/Helpers";
import { EMAIL_REGEX, NAME_REGEX, USERNAME_REGEX } from "./authValidation";

/**
 * Validates password match
 * @param {string} password - The password
 * @param {string} confirmPassword - The confirmation password
 * @returns {Object} Response object with success flag
 */
 const validatePasswordMatch = (password, confirmPassword) => {
  if (!confirmPassword || typeof confirmPassword !== 'string') {
    return failResponse(
      "Invalid password confirmation",
      ["Password confirmation is required"]
    );
  }

  if (password !== confirmPassword) {
    return failResponse(
      "Password mismatch",
      ["The password and confirmation password do not match"]
    );
  }

  return successResponse({ password, confirmPassword }, "Passwords match");
};

/**
 * Validates login data
 * @param {Object} loginData - The login data to validate
 * @returns {Object} Response object with success flag
 */
export const validateLoginData = (loginData) => {
  // Validate email
  const emailResult = validateString(loginData?.email, "Email", {
    maxLength: 320,
    pattern: EMAIL_REGEX,
    patternMessage: "Email must be in a valid format"
  });
  if (!emailResult.success) {
    return emailResult;
  }
   
  // Validate password
  const passwordResult = validateString(loginData?.password, "Password", {
    minLength: 8,
    maxLength: 100
  });
  if (!passwordResult.success) {
    return passwordResult;
  }
  
  return successResponse(loginData, "Login data is valid");
};

/**
 * Validates registration data
 * @param {Object} registrationData - The registration data to validate
 * @returns {Object} Response object with success flag
 */
export const validateRegistrationData = (registrationData) => {
  // Validate first name
  const firstNameResult = validateString(registrationData?.firstName, "First Name", {
    minLength: 2,
    maxLength: 50,
    pattern: NAME_REGEX,
    patternMessage: "First name can only contain letters and spaces"
  });
  if (!firstNameResult.success) {
    return firstNameResult;
  }
  
  // Validate last name
  const lastNameResult = validateString(registrationData?.lastName, "Last Name", {
    minLength: 2,
    maxLength: 50,
    pattern: NAME_REGEX,
    patternMessage: "Last name can only contain letters and spaces"
  });
  if (!lastNameResult.success) {
    return lastNameResult;
  }
  
  // Validate city ID
  const cityIdResult = validateId(registrationData?.cityId, "City ID");
  if (!cityIdResult.success) {
    return cityIdResult;
  }
  
  // Validate username
  const usernameResult = validateString(registrationData?.userName, "Username", {
    minLength: 3,
    maxLength: 50,
    pattern: USERNAME_REGEX,
    patternMessage: "Username can only contain letters, numbers, dots, and underscores"
  });
  if (!usernameResult.success) {
    return usernameResult;
  }
  
  // Validate email
  const emailResult = validateString(registrationData?.email, "Email", {
    maxLength: 320,
    pattern: EMAIL_REGEX,
    patternMessage: "Email must be in a valid format"
  });
  if (!emailResult.success) {
    return emailResult;
  }
  
  // Validate password
  const passwordResult = validateString(registrationData?.password, "Password", {
    minLength: 8,
    maxLength: 100
  });
  if (!passwordResult.success) {
    return passwordResult;
  }
  
  // Validate password match
  const passwordMatchResult = validatePasswordMatch(
    registrationData?.password, 
    registrationData?.confirmPassword
  );
  if (!passwordMatchResult.success) {
    return passwordMatchResult;
  }

  return successResponse(registrationData, "Registration data is valid");
};