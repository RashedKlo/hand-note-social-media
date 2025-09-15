using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Message.Add
{
    public class MarkMessagesAsReadResponseDto
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public int MessagesMarkedAsRead { get; set; }
    }
}