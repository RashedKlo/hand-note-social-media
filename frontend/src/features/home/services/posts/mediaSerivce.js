import { handleApi } from "../../../services/api/apiHandler";
import { validateUploadMedia } from "../utils/mediaValidation";
import { mediaApi } from "./mediaApi";

export const mediaService = {
  /**
   * Upload media files
   */
  uploadMedia: async (userId, filePaths) =>
    handleApi(
      () => mediaApi.uploadMedia(userId, filePaths),
      () => validateUploadMedia(userId, filePaths)
    ),
};