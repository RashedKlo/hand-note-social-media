using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IMessageService
    {
        Task<OperationResult<MessageAddResponseDto>> AddMessageAsync(MessageAddRequestDto dto);
        Task<OperationResult<MessagesReadResponseDto>> MarkMessagesAsReadAsync(MessagesReadRequestDto dto);
        Task<OperationResult<List<MessagesGetResponseDto>>> GetMessagesAsync(MessagesGetRequestDto dto);

    }
}