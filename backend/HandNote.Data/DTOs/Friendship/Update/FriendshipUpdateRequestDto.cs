using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Models;

namespace HandNote.Data.DTOs.Friendship.Update
{
    public class FriendshipUpdateRequestDto
    {
        [Required(ErrorMessage = "FriendshipId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "FriendshipId must be a positive integer.")]
        public int FriendshipId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "NewStatus is required.")]
        [RegularExpression("^(pending|accepted|declined|blocked|cancelled)$",
            ErrorMessage = "Invalid status. Must be: pending, accepted, declined, blocked, or cancelled.")]
        public required EnFriendshipStatus NewStatus { get; set; }
    }
}