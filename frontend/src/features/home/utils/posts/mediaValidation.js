import { failResponse, successResponse } from "../../../services/api/apiHandler";
import { validateId } from "../../../utils/Helpers";

/**
 * Validates upload media request
 */
export const validateUploadMedia = (userId, filePaths) => {
  // Validate userId
  const userResult = validateId(userId, "User ID");
  if (!userResult.success) {
    return userResult;
  }

  // Validate filePaths
  if (!Array.isArray(filePaths) || filePaths.length === 0) {
    return failResponse(
      "Invalid file paths",
      ["At least one file path is required"]
    );
  }

  // Validate each file path
  for (const path of filePaths) {
    if (typeof path !== 'string' || path.trim() === '') {
      return failResponse(
        "Invalid file path",
        ["All file paths must be non-empty strings"]
      );
    }
  }

  return successResponse({ userId, filePaths }, "Upload media data is valid");
};