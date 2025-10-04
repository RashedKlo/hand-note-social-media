import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateResetPrivacySettings,
  validateUpdatePrivacySettings
} from "../utils/privacyValidation";
import { privacyApi } from "./privacyApi";

export const privacyService = {
  /**
   * Reset user privacy settings
   */
  resetPrivacySettings: async (userId) =>
    handleApi(
      () => privacyApi.resetPrivacySettings(userId),
      () => validateResetPrivacySettings(userId)
    ),

  /**
   * Update user privacy settings
   */
  updatePrivacySettings: async (userId, settings) =>
    handleApi(
      () => privacyApi.updatePrivacySettings(userId, settings),
      () => validateUpdatePrivacySettings(userId, settings)
    ),
};

