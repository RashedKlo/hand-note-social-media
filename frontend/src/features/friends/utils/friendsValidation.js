import { failResponse } from "../../../services/api/apiHandler";



/**
 * Validates that two user IDs are different
 */
export const validateDifferentUsers = (userId1, userId2) => {
  if (userId1 === userId2) {
    return failResponse(
      "Invalid operation",
      ["Cannot perform this action with the same user"]
    );
  }
  return null;
};