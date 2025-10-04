import { handleApi } from "../../../services/api/apiHandler";
import { 
  validateCreateConversation,
  validateCheckMembership
} from "../utils/conversationsValidation";
import { conversationsApi } from "./conversationsApi";

export const conversationsService = {
  /**
   * Create a new conversation
   */
  createConversation: async (currentUserId, friendId) =>
    handleApi(
      () => conversationsApi.createConversation(currentUserId, friendId),
      () => validateCreateConversation(currentUserId, friendId)
    ),

  /**
   * Check user membership in conversation
   */
  checkUserMembership: async (conversationId, userId) =>
    handleApi(
      () => conversationsApi.checkUserMembership(conversationId, userId),
      () => validateCheckMembership(conversationId, userId)
    ),
};