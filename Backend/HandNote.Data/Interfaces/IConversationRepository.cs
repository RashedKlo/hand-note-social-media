using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IConversationRepository
    {

        Task<OperationResult<ConversationCreateResponseDto>> CreateConversationAsync(ConversationCreateRequestDto dto);

        /// <summary>
        /// Checks if a user is part of a specific conversation with detailed response
        /// </summary>
        /// <param name="dto">Request containing conversation ID and user ID</param>
        /// <returns>Detailed conversation existence information</returns>
        Task<OperationResult<ConversationExistenceResponseDto>> IsUserPartOfConversationAsync(ConversationExistenceRequestDto dto);

    }
}