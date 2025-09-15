using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Conversation
{
    public class CreateConversationRequestDto
    {
        [Required(ErrorMessage = "CurrentUserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CurrentUserId must be a positive integer.")]
        public int CurrentUserId { get; set; }

        [Required(ErrorMessage = "FriendId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "FriendId must be a positive integer.")]
        public int FriendId { get; set; }
    }
}