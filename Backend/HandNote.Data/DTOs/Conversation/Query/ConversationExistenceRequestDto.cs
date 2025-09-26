using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Conversation.Query
{
    /// <summary>
    /// Request DTO for sp_IsUserPartOfConversation stored procedure
    /// </summary>
    public class ConversationExistenceRequestDto
    {
        [Required(ErrorMessage = "ConversationId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ConversationId must be a positive integer.")]
        public int ConversationId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }
    }
}