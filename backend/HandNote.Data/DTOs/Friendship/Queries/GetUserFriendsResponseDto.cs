using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
    public class GetUserFriendsResponseDto
    {
        public int FriendId { get; set; }
        public string FriendUsername { get; set; } = string.Empty;
        public string FriendFirstName { get; set; } = string.Empty;
        public string FriendLastName { get; set; } = string.Empty;
        public string? FriendProfilePicture { get; set; }
        public int? ConversationId { get; set; }
        public DateTime? LastActivity { get; set; }
        public string? LastMessageContent { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public int? LastMessageSenderId { get; set; }
        public bool IsLastMessageFromMe { get; set; }
        public int UnreadCount { get; set; }
    }
}