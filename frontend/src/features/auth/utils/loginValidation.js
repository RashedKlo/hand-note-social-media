import { failResponse } from "../../../services/api/apiHandler";
import { validateEmail, validatePassword } from "./authValidation";

export const validateLoginData = (loginData) => {
  const errors = [];
  
  // Validate email
  const emailError = validateEmail(loginData?.email);
  if (emailError) {
    errors.push(...emailError.errors);
  }
   
  // Validate password
  const passwordError = validatePassword(loginData?.password);
  if (passwordError) {
    errors.push(...passwordError.errors);
  }
  
  if (errors.length > 0) {
    return failResponse("Invalid login data", errors);
  }
  
  return null;
};