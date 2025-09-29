using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.Conversation.Commands;
using HandNote.Data.Repositories.Conversation.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Conversation
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ILogger<ConversationRepository> _logger;

        public ConversationRepository(ILogger<ConversationRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<ConversationCreateResponseDto>> CreateConversationAsync(ConversationCreateRequestDto dto)
        {
            _logger.LogInformation("ConversationRepository.CreateConversationAsync called for CurrentUserId: {CurrentUserId}, FriendId: {FriendId}",
                dto.CurrentUserId, dto.FriendId);

            return await CreateConversationCommand.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<ConversationExistenceResponseDto>> IsUserPartOfConversationAsync(ConversationExistenceRequestDto dto)
        {
            _logger.LogInformation("ConversationRepository.IsUserPartOfConversationAsync called for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);



            var result = await IsUserPartOfConversationQuery.ExecuteAsync(dto, _logger);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("IsUserPartOfConversationAsync failed for ConversationId {ConversationId}, UserId {UserId}: {Message}",
                    dto.ConversationId, dto.UserId, result.Message);
                return OperationResult<ConversationExistenceResponseDto>.Failure(result.Message);
            }

            var isUserPart = result.Data?.IsUserPartOfConversation ?? false;

            _logger.LogDebug("IsUserPartOfConversationAsync completed - ConversationId: {ConversationId}, UserId: {UserId}, IsUserPart: {IsUserPart}",
                dto.ConversationId, dto.UserId, isUserPart);

            return OperationResult<ConversationExistenceResponseDto>.Success(result.Data!, result.Message);
        }

    }
}