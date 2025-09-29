using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IConversationService
    {
        Task<OperationResult<ConversationCreateResponseDto>> CreateConversationAsync(ConversationCreateRequestDto dto);
        Task<OperationResult<ConversationExistenceResponseDto>> IsUserPartOfConversationAsync(ConversationExistenceRequestDto dto);
    }
}