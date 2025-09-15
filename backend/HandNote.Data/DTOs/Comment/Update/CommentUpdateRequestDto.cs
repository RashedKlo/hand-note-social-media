using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Update
{
    // Comment Update Request DTO
    public class CommentUpdateRequestDto
    {
        [Required(ErrorMessage = "CommentId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CommentId must be a positive integer.")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 200 characters.")]
        public required string Content { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MediaId must be a positive integer.")]
        public int? MediaId { get; set; }
    }
}