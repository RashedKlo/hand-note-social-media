using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Conversation
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ILogger<ConversationService> _logger;

        public ConversationService(
            IConversationRepository conversationRepository,
            ILogger<ConversationService> logger)
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<ConversationCreateResponseDto>> CreateConversationAsync(ConversationCreateRequestDto dto)
        {
            var validationResult = ConversationValidation.ValidateConversationCreationAsync(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Conversation validation failed for CurrentUserId {CurrentUserId}, FriendId {FriendId}: {Message}",
                    dto.CurrentUserId, dto.FriendId, validationResult.Message);
                return OperationResult<ConversationCreateResponseDto>.Failure(validationResult.Message);
            }
            _logger.LogInformation("Processing conversation creation for CurrentUserId: {CurrentUserId}, FriendId: {FriendId}",
                dto.CurrentUserId, dto.FriendId);

            var conversationResult = await _conversationRepository.CreateConversationAsync(dto);

            if (!conversationResult.IsSuccess)
            {
                _logger.LogWarning("Conversation creation failed for CurrentUserId {CurrentUserId}, FriendId {FriendId}: {Message}",
                    dto.CurrentUserId, dto.FriendId, conversationResult.Message);
                return OperationResult<ConversationCreateResponseDto>.Failure(conversationResult.Message);
            }

            _logger.LogInformation("Conversation creation completed successfully - ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, FriendId: {FriendId}, Friend: {FriendUsername}",
                conversationResult.Data?.ConversationId, dto.CurrentUserId, dto.FriendId, conversationResult.Data?.FriendUsername);

            return OperationResult<ConversationCreateResponseDto>.Success(conversationResult.Data!, conversationResult.Message);
        }
        public async Task<OperationResult<ConversationExistenceResponseDto>> IsUserPartOfConversationAsync(ConversationExistenceRequestDto dto)
        {
            var validationResult = ConversationValidation.ValidateConversationExistenceAsyn(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Conversation Existence validation failed for ConversationID {ConversationID}, UserId {UserId}: {Error}",
                    dto.ConversationId, dto.UserId, validationResult.Message);
                return OperationResult<ConversationExistenceResponseDto>.Failure(validationResult.Message);
            }

            _logger.LogInformation("Processing detailed user part of conversation check for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);

            var checkResult = await _conversationRepository.IsUserPartOfConversationAsync(dto);

            if (!checkResult.IsSuccess)
            {
                _logger.LogWarning("Detailed user part of conversation check failed for ConversationId {ConversationId}, UserId {UserId}: {Message}",
                    dto.ConversationId, dto.UserId, checkResult.Message);
                return OperationResult<ConversationExistenceResponseDto>.Failure(checkResult.Message);
            }

            _logger.LogInformation("Detailed user part of conversation check completed successfully - ConversationId: {ConversationId}, UserId: {UserId}, ConversationExists: {ConversationExists}, IsUserPart: {IsUserPart}",
                dto.ConversationId, dto.UserId, checkResult.Data?.ConversationExists, checkResult.Data?.IsUserPartOfConversation);

            return OperationResult<ConversationExistenceResponseDto>.Success(checkResult.Data!, checkResult.Message);
        }

    }
}