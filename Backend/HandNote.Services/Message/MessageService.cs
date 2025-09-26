using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Message
{
    public sealed class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository,
            ILogger<MessageService> logger)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<MessageAddResponseDto>> AddMessageAsync(MessageAddRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Message addition attempted with null data");
                return OperationResult<MessageAddResponseDto>.Failure("Message data is required");
            }

            _logger.LogInformation("Processing message addition for ConversationId: {ConversationId}, SenderId: {SenderId}, MessageType: {MessageType}",
                dto.ConversationId, dto.SenderId, dto.MessageType);

            var validationResult = await MessageValidation.ValidateMessageAdditionAsync(dto, _conversationRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Message addition validation failed for ConversationId {ConversationId}, SenderId {SenderId}: {Error}",
                    dto.ConversationId, dto.SenderId, validationResult.Message);
                return OperationResult<MessageAddResponseDto>.Failure(validationResult.Message);
            }

            var addResult = await _messageRepository.AddMessageAsync(dto);

            if (!addResult.IsSuccess)
            {
                _logger.LogWarning("Message addition failed for ConversationId {ConversationId}, SenderId {SenderId}: {Message}",
                    dto.ConversationId, dto.SenderId, addResult.Message);
                return OperationResult<MessageAddResponseDto>.Failure(addResult.Message);
            }

            _logger.LogInformation("Message added successfully - MessageId: {MessageId}, ConversationId: {ConversationId}, SenderId: {SenderId}, MessageType: {MessageType}",
                addResult.Data?.MessageId, dto.ConversationId, dto.SenderId, dto.MessageType);

            return OperationResult<MessageAddResponseDto>.Success(addResult.Data!, addResult.Message);
        }

        public async Task<OperationResult<MessagesReadResponseDto>> MarkMessagesAsReadAsync(MessagesReadRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Mark messages as read attempted with null data");
                return OperationResult<MessagesReadResponseDto>.Failure("Message data is required");
            }

            _logger.LogInformation("Processing mark messages as read for ConversationId: {ConversationId}, UserId: {UserId}",
                dto.ConversationId, dto.UserId);

            var validationResult = await MessageValidation.ValidateMarkMessagesAsReadAsync(dto, _conversationRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Mark messages as read validation failed for ConversationId {ConversationId}, UserId {UserId}: {Error}",
                    dto.ConversationId, dto.UserId, validationResult.Message);
                return OperationResult<MessagesReadResponseDto>.Failure(validationResult.Message);
            }

            var markReadResult = await _messageRepository.MarkMessagesAsReadAsync(dto);

            if (!markReadResult.IsSuccess)
            {
                _logger.LogWarning("Mark messages as read failed for ConversationId {ConversationId}, UserId {UserId}: {Message}",
                    dto.ConversationId, dto.UserId, markReadResult.Message);
                return OperationResult<MessagesReadResponseDto>.Failure(markReadResult.Message);
            }

            _logger.LogInformation("Messages marked as read successfully - ConversationId: {ConversationId}, UserId: {UserId}, Messages Marked: {MessagesMarkedAsRead}",
                dto.ConversationId, dto.UserId, markReadResult.Data?.MessagesMarkedAsRead);

            return OperationResult<MessagesReadResponseDto>.Success(markReadResult.Data!, markReadResult.Message);
        }

        public async Task<OperationResult<List<MessagesGetResponseDto>>> GetMessagesAsync(MessagesGetRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Get messages attempted with null data");
                return OperationResult<List<MessagesGetResponseDto>>.Failure("Message data is required");
            }

            _logger.LogInformation("Processing get messages for ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, PageSize: {PageSize}, PageNumber: {PageNumber}",
                dto.ConversationId, dto.CurrentUserId, dto.PageSize, dto.PageNumber);

            var validationResult = await MessageValidation.ValidateGetMessagesAsync(dto, _conversationRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Get messages validation failed for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}: {Error}",
                    dto.ConversationId, dto.CurrentUserId, validationResult.Message);
                return OperationResult<List<MessagesGetResponseDto>>.Failure(validationResult.Message);
            }

            var getResult = await _messageRepository.GetMessagesAsync(dto);

            if (!getResult.IsSuccess)
            {
                _logger.LogWarning("Get messages failed for ConversationId {ConversationId}, CurrentUserId {CurrentUserId}: {Message}",
                    dto.ConversationId, dto.CurrentUserId, getResult.Message);
                return OperationResult<List<MessagesGetResponseDto>>.Failure(getResult.Message);
            }

            _logger.LogInformation("Messages retrieved successfully - ConversationId: {ConversationId}, CurrentUserId: {CurrentUserId}, Messages Count: {MessageCount}",
                dto.ConversationId, dto.CurrentUserId, getResult.Data?.Count ?? 0);

            return OperationResult<List<MessagesGetResponseDto>>.Success(getResult.Data!, getResult.Message);
        }


    }
}