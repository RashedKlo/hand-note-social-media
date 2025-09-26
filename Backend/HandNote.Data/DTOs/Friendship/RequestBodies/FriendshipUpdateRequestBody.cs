using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.RequestBodies
{

    public class FriendshipUpdateRequestBody
    {

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "NewStatus is required.")]
        [RegularExpression("^(pending|accepted|declined|blocked|cancelled)$",
            ErrorMessage = "Invalid status. Must be: pending, accepted, declined, blocked, or cancelled.")]
        public required string NewStatus { get; set; }
    }
}