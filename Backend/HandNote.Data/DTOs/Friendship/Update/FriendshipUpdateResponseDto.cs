using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Update
{
    public class FriendshipUpdateResponseDto
    {
        public int FriendshipId { get; set; }

        public int RequesterId { get; set; }

        public int AddresseeId { get; set; }

        public required string FriendshipStatus { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}