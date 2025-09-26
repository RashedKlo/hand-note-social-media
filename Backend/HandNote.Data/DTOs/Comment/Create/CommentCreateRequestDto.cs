using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Create
{
    public class CommentCreateRequestDto
    {
        [Required(ErrorMessage = "PostId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PostId must be a positive integer.")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ParentCommentId must be a positive integer.")]
        public int? ParentCommentId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 200 characters.")]
        public string Content { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "MediaId must be a positive integer.")]
        public int? MediaId { get; set; }
    }
}