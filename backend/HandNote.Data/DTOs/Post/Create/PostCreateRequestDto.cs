using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Post.Create
{
    public class PostCreateRequestDto
    {
        [Required(ErrorMessage = "UserId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer")]
        public int UserId { get; set; }

        [StringLength(1200, ErrorMessage = "Content cannot exceed 1200 characters")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "PostType is required")]
        [RegularExpression("^(text|media|shared)$", ErrorMessage = "PostType must be: text, media, or shared")]
        public string PostType { get; set; } = "text";

        [Range(1, int.MaxValue, ErrorMessage = "SharedPostId must be a positive integer")]
        public int? SharedPostId { get; set; }

        public bool HasMedia { get; set; } = false;

        public bool AllowsComments { get; set; } = true;

        public bool AllowsShares { get; set; } = true;

    }
}