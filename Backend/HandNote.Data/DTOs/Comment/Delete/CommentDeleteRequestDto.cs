using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Delete
{
    public class CommentDeleteRequestDto
    {
        [Required(ErrorMessage = "CommentId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CommentId must be a positive integer.")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }
    }
}