using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Message.Add
{
    public class MessageAddRequestDto
    {
        [Required(ErrorMessage = "ConversationId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ConversationId must be a positive integer.")]
        public int ConversationId { get; set; }

        [Required(ErrorMessage = "SenderId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SenderId must be a positive integer.")]
        public int SenderId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 2000 characters.")]
        public string Content { get; set; } = string.Empty;

        [RegularExpression("^(text|image|file|system)$", ErrorMessage = "MessageType must be: text, image, file, or system.")]
        public string MessageType { get; set; } = "text";

        [Range(1, int.MaxValue, ErrorMessage = "MediaId must be a positive integer.")]
        public int? MediaId { get; set; }
    }
}