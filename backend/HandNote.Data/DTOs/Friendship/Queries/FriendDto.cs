using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
    public class FriendDto
    {
        public int FriendId { get; set; }
        public string FriendUsername { get; set; } = string.Empty;
        public string FriendFirstName { get; set; } = string.Empty;
        public string FriendLastName { get; set; } = string.Empty;
        public string? FriendProfilePicture { get; set; }
        public string FriendEmail { get; set; } = string.Empty;
        public DateTime? FriendsSince { get; set; }
        public int FriendshipId { get; set; }
    }
}