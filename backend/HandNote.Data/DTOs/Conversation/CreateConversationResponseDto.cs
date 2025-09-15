using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Conversation
{
    public class CreateConversationResponseDto
    {
          public int ConversationId { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastActivity { get; set; }
        public bool IsActive { get; set; }
        public string FriendUsername { get; set; } = string.Empty;
        public string FriendFirstName { get; set; } = string.Empty;
        public string? FriendProfilePicture { get; set; }
    }
}