using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Conversation
{
    public class ConversationValidation
    {
        public static OperationResult<bool> ValidateConversationCreationAsync(ConversationCreateRequestDto dto, ILogger logger)
        {
            if (dto.CurrentUserId <= 0)
            {
                logger.LogWarning("Create conversation attempted with invalid CurrentUserId: {CurrentUserId}", dto.CurrentUserId);
                return OperationResult<bool>.Failure("CurrentUserId must be a positive integer");
            }

            if (dto.FriendId <= 0)
            {
                logger.LogWarning("Create conversation attempted with invalid FriendId: {FriendId}", dto.FriendId);
                return OperationResult<bool>.Failure("FriendId must be a positive integer");
            }

            if (dto.CurrentUserId == dto.FriendId)
            {
                logger.LogWarning("Create conversation attempted with same user IDs: CurrentUserId: {CurrentUserId}, FriendId: {FriendId}",
                    dto.CurrentUserId, dto.FriendId);
                return OperationResult<bool>.Failure("Cannot create conversation with yourself");
            }
            return OperationResult<bool>.Success(true, "Validation Passed");

        }
        public static OperationResult<bool> ValidateConversationExistenceAsyn(ConversationExistenceRequestDto dto, ILogger logger)
        {
            if (dto == null)
            {
                logger.LogWarning("Check user part of conversation attempted with null data");
                return OperationResult<bool>.Failure("Conversation existence data is required");
            }

            if (dto.ConversationId <= 0)
            {
                logger.LogWarning("Check user part of conversation attempted with invalid ConversationId: {ConversationId}", dto.ConversationId);
                return OperationResult<bool>.Failure("ConversationId must be a positive integer");
            }

            if (dto.UserId <= 0)
            {
                logger.LogWarning("Check user part of conversation attempted with invalid UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("UserId must be a positive integer");
            }
            return OperationResult<bool>.Success(true, "validation passed");
        }
    }
}