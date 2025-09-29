using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
     public class FriendRequestDto
    {
        public int RequesterId { get; set; }
        public string RequesterUsername { get; set; } = string.Empty;
        public string RequesterFirstName { get; set; } = string.Empty;
        public string RequesterLastName { get; set; } = string.Empty;
        public string? RequesterProfilePicture { get; set; }
        public string RequesterEmail { get; set; } = string.Empty;
        public DateTime? RequestSentAt { get; set; }
        public int FriendshipId { get; set; }
    }

}