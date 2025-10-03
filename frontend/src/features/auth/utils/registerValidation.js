import { failResponse } from "../../../services/api/apiHandler";
import { validateCityId, validateEmail, validateFirstName, validateLastName, validatePassword, validatePasswordMatch, validateUsername } from "./authValidation";

export const validateRegistrationData = (registrationData) => {
  const errors = [];
  
  // Validate first name
  const firstNameError = validateFirstName(registrationData?.firstName);
  if (firstNameError) {
    errors.push(...firstNameError.errors);
  }
  
  // Validate last name
  const lastNameError = validateLastName(registrationData?.lastName);
  if (lastNameError) {
    errors.push(...lastNameError.errors);
  }
  
  // Validate city ID
  const cityIdError = validateCityId(registrationData?.cityId);
  if (cityIdError) {
    errors.push(...cityIdError.errors);
  }
  
  // Validate username
  const usernameError = validateUsername(registrationData?.userName);
  if (usernameError) {
    errors.push(...usernameError.errors);
  }
  
  // Validate email
  const emailError = validateEmail(registrationData?.email);
  if (emailError) {
    errors.push(...emailError.errors);
  }
  
  // Validate password
  const passwordError = validatePassword(registrationData?.password);
  if (passwordError) {
    errors.push(...passwordError.errors);
  }
  
  // Validate password match
  const passwordMatchError = validatePasswordMatch(
    registrationData?.password, 
    registrationData?.confirmPassword
  );
  if (passwordMatchError) {
    errors.push(...passwordMatchError.errors);
  }
  
  if (errors.length > 0) {
    return failResponse("Invalid registration data", errors);
  }
  
  return null;
};