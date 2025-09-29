using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Conversation.Query
{
    /// <summary>
    /// Response DTO for sp_IsUserPartOfConversation stored procedure
    /// </summary>
    public class ConversationExistenceResponseDto
    {
        /// <summary>
        /// ID of the conversation being checked (NULL for errors)
        /// </summary>
        public int ConversationId { get; set; }

        /// <summary>
        /// ID of the user being checked (NULL for errors)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Whether the conversation exists (NULL for errors)
        /// </summary>
        public bool ConversationExists { get; set; }

        /// <summary>
        /// Whether the user is part of the conversation (NULL for errors)
        /// </summary>
        public bool IsUserPartOfConversation { get; set; }

    }
}