using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.Message.Commands;
using HandNote.Data.Repositories.Message.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ILogger<MessageRepository> _logger;
        public MessageRepository(ILogger<MessageRepository> logger)
        {
            this._logger = logger;
        }
        public async Task<OperationResult<MessageAddResponseDto>> AddMessageAsync(MessageAddRequestDto dto)
        {
            return await AddMessageCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<MessagesReadResponseDto>> MarkMessagesAsReadAsync(MessagesReadRequestDto dto)
        {
            return await MarkMessageAsReadCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<List<MessagesGetResponseDto>>> GetMessagesAsync(MessagesGetRequestDto dto)
        {
            return await GetMessagesQuery.ExecuteAsync(dto, _logger);
        }

    }
}