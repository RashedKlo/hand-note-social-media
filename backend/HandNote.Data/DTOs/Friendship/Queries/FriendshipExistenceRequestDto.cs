using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
    public class FriendshipExistenceRequestDto
    {
        [Required(ErrorMessage = "UserId1 is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId1 must be a positive integer.")]
        public int UserId1 { get; set; }

        [Required(ErrorMessage = "UserId2 is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId2 must be a positive integer.")]
        public int UserId2 { get; set; }
    }
}