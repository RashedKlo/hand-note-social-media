using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Message.Get
{
    public class GetMessagesRequestDto
    {
        [Required(ErrorMessage = "ConversationId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ConversationId must be a positive integer.")]
        public int ConversationId { get; set; }

        [Required(ErrorMessage = "CurrentUserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CurrentUserId must be a positive integer.")]
        public int CurrentUserId { get; set; }

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 20;

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
        public int PageNumber { get; set; } = 1;
    }
}