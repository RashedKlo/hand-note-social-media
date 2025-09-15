using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
    public class FriendshipExistenceResponseDto
    {
        public int? FriendshipId { get; set; }

        public int UserId1 { get; set; }

        public int UserId2 { get; set; }

        public bool? FriendshipExists { get; set; }

        public required string FriendshipStatus { get; set; }

    }
}