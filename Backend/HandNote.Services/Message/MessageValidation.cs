using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Message
{
    public class MessageValidation
    {
        public static async Task<OperationResult> ValidateMessageAdditionAsync(MessageAddRequestDto dto, IConversationRepository conversationRepository, ILogger logger)
        {
            logger.LogDebug("Starting message addition validation for ConversationId: {ConversationId}, SenderId: {SenderId}",
                dto.ConversationId, dto.SenderId);

            // Validate conversation exists and user is part of it
            var conversationExists = await conversationRepository.IsUserPartOfConversationAsync(new ConversationExistenceRequestDto { ConversationId = dto.ConversationId, UserId = dto.SenderId });
            if (!conversationExists.IsSuccess)
            {
                logger.LogError("Failed to validate conversation access for ConversationId: {ConversationId}, SenderId: {SenderId}. Repository error: {Error}",
                    dto.ConversationId, dto.SenderId, conversationExists.Message);
                return OperationResult.Failure("Failed to validate conversation access");
            }

            if (!conversationExists.Data!.IsUserPartOfConversation)
            {
                logger.LogWarning("User {SenderId} is not part of conversation {ConversationId}, message addition denied",
                    dto.SenderId, dto.ConversationId);
                return OperationResult.Failure("User is not part of the specified conversation");
            }

            logger.LogDebug("Message addition validation passed for ConversationId: {ConversationId}, SenderId: {SenderId}",
                dto.ConversationId, dto.SenderId);

            // Additional validations can be added here (content filtering, rate limiting, etc.)
            return OperationResult.Success();
        }

        public static async Task<OperationResult> ValidateMarkMessagesAsReadAsync(MessagesReadRequestDto dto, IConversationRepository conversationRepository, ILogger logger)
        {
            logger.LogDebug("Starting mark messages as read validation for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);

            // Validate conversation exists and user is part of it
            var conversationExists = await conversationRepository.IsUserPartOfConversationAsync(new ConversationExistenceRequestDto { ConversationId = dto.ConversationId, UserId = dto.UserId });
            if (!conversationExists.IsSuccess)
            {
                logger.LogError("Failed to validate conversation access for ConversationId: {ConversationId}, UserId: {UserId}. Repository error: {Error}",
                    dto.ConversationId, dto.UserId, conversationExists.Message);
                return OperationResult.Failure("Failed to validate conversation access");
            }

            if (!conversationExists.Data!.IsUserPartOfConversation)
            {
                logger.LogWarning("User {UserId} is not part of conversation {ConversationId}, mark as read denied",
                    dto.UserId, dto.ConversationId);
                return OperationResult.Failure("User is not part of the specified conversation");
            }

            logger.LogDebug("Mark messages as read validation passed for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);

            return OperationResult.Success();
        }

        public static async Task<OperationResult> ValidateGetMessagesAsync(MessagesGetRequestDto dto, IConversationRepository conversationRepository, ILogger logger)
        {
            logger.LogDebug("Starting get messages validation for ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, PageSize: {PageSize}, PageNumber: {PageNumber}",
                dto.ConversationId, dto.CurrentUserId, dto.PageSize, dto.PageNumber);

            // Validate conversation exists and user is part of it
            var conversationExists = await conversationRepository.IsUserPartOfConversationAsync(new ConversationExistenceRequestDto { ConversationId = dto.ConversationId, UserId = dto.CurrentUserId });
            if (!conversationExists.IsSuccess)
            {
                logger.LogError("Failed to validate conversation access for ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}. Repository error: {Error}",
                    dto.ConversationId, dto.CurrentUserId, conversationExists.Message);
                return OperationResult.Failure("Failed to validate conversation access");
            }

            if (!conversationExists.Data!.IsUserPartOfConversation!)
            {
                logger.LogWarning("User {CurrentUserId} is not part of conversation {ConversationId}, get messages denied",
                    dto.CurrentUserId, dto.ConversationId);
                return OperationResult.Failure("User is not part of the specified conversation");
            }

            logger.LogDebug("Get messages validation passed for ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}",
                dto.ConversationId, dto.CurrentUserId);

            return OperationResult.Success();
        }
    }
}