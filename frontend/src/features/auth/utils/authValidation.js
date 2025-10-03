import { failResponse } from "../../../services/api/apiHandler";

/**
 * Email validation regex
 */
const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

/**
 * Name validation regex (letters and spaces only)
 */
const NAME_REGEX = /^[a-zA-Z\s]+$/;

/**
 * Username validation regex (letters, numbers, dots, underscores)
 */
const USERNAME_REGEX = /^[a-zA-Z0-9._]+$/;

/**
 * Validates first name
 */
export const validateFirstName = (firstName) => {
  if (!firstName || typeof firstName !== 'string' || firstName.trim() === '') {
    return failResponse(
      "Invalid first name",
      ["First name is required and must be a non-empty string"]
    );
  }

  if (firstName.length < 2) {
    return failResponse(
      "Invalid first name",
      ["First name must be at least 2 characters long"]
    );
  }

  if (firstName.length > 50) {
    return failResponse(
      "Invalid first name",
      ["First name must not exceed 50 characters"]
    );
  }

  if (!NAME_REGEX.test(firstName)) {
    return failResponse(
      "Invalid first name",
      ["First name can only contain letters and spaces"]
    );
  }

  return null;
};

/**
 * Validates last name
 */
export const validateLastName = (lastName) => {
  if (!lastName || typeof lastName !== 'string' || lastName.trim() === '') {
    return failResponse(
      "Invalid last name",
      ["Last name is required and must be a non-empty string"]
    );
  }

  if (lastName.length < 2) {
    return failResponse(
      "Invalid last name",
      ["Last name must be at least 2 characters long"]
    );
  }

  if (lastName.length > 50) {
    return failResponse(
      "Invalid last name",
      ["Last name must not exceed 50 characters"]
    );
  }

  if (!NAME_REGEX.test(lastName)) {
    return failResponse(
      "Invalid last name",
      ["Last name can only contain letters and spaces"]
    );
  }

  return null;
};

/**
 * Validates city ID
 */
export const validateCityId = (cityId) => {
  if (!cityId) {
    return failResponse(
      "Invalid city ID",
      ["City ID is required"]
    );
  }

  if (typeof cityId !== 'number' || !Number.isInteger(cityId)) {
    return failResponse(
      "Invalid city ID",
      ["City ID must be a number"]
    );
  }

  if (cityId < 1) {
    return failResponse(
      "Invalid city ID",
      ["City ID must be a positive number"]
    );
  }

  return null;
};

/**
 * Validates email format
 */
export const validateEmail = (email) => {
  if (!email || typeof email !== 'string' || email.trim() === '') {
    return failResponse(
      "Invalid email",
      ["Email is required and must be a non-empty string"]
    );
  }

  if (!EMAIL_REGEX.test(email)) {
    return failResponse(
      "Invalid email",
      ["Email must be in a valid format"]
    );
  }

  if (email.length > 320) {
    return failResponse(
      "Invalid email",
      ["Email must not exceed 320 characters"]
    );
  }

  return null;
};

/**
 * Validates password
 */
export const validatePassword = (password, fieldName = "Password") => {
  if (!password || typeof password !== 'string') {
    return failResponse(
      `Invalid ${fieldName.toLowerCase()}`,
      [`${fieldName} is required and must be a string`]
    );
  }

  if (password.length < 8) {
    return failResponse(
      `Invalid ${fieldName.toLowerCase()}`,
      [`${fieldName} must be at least 8 characters long`]
    );
  }

  if (password.length > 100) {
    return failResponse(
      `Invalid ${fieldName.toLowerCase()}`,
      [`${fieldName} must not exceed 100 characters`]
    );
  }

  return null;
};

/**
 * Validates username
 */
export const validateUsername = (userName) => {
  if (!userName || typeof userName !== 'string' || userName.trim() === '') {
    return failResponse(
      "Invalid username",
      ["Username is required and must be a non-empty string"]
    );
  }

  if (userName.length < 3) {
    return failResponse(
      "Invalid username",
      ["Username must be at least 3 characters long"]
    );
  }

  if (userName.length > 50) {
    return failResponse(
      "Invalid username",
      ["Username must not exceed 50 characters"]
    );
  }

  if (!USERNAME_REGEX.test(userName)) {
    return failResponse(
      "Invalid username",
      ["Username can only contain letters, numbers, dots, and underscores"]
    );
  }

  return null;
};

/**
 * Validates password confirmation
 */
export const validatePasswordMatch = (password, confirmPassword) => {
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

  return null;
};



