import { failResponse, successResponse } from "../../../services/api/apiHandler";
import { validateId } from "../../../utils/Helpers";

/**
 * Validates create conversation request
 */
export const validateCreateConversation = (currentUserId, friendId) => {
  // Validate currentUserId
  const currentUserResult = validateId(currentUserId, "Current User ID");
  if (!currentUserResult.success) {
    return currentUserResult;
  }

  // Validate friendId
  const friendResult = validateId(friendId, "Friend ID");
  if (!friendResult.success) {
    return friendResult;
  }

  // Validate they are different users
  if (currentUserId === friendId) {
    return failResponse(
      "Invalid conversation",
      ["Cannot create a conversation with yourself"]
    );
  }

  return successResponse({ currentUserId, friendId }, "Conversation data is valid");
};

/**
 * Validates check membership request
 */
export const validateCheckMembership = (conversationId, userId) => {
  // Validate conversationId
  const conversationResult = validateId(conversationId, "Conversation ID");
  if (!conversationResult.success) {
    return conversationResult;
  }

  // Validate userId
  const userResult = validateId(userId, "User ID");
  if (!userResult.success) {
    return userResult;
  }

  return successResponse({ conversationId, userId }, "Membership check data is valid");
};
