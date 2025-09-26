using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.CommentReaction.Create
{
    /// <summary>
    /// DTO for creating a new comment reaction
    /// </summary>
    public class CommentReactionCreateRequestDto
    {
        [Required(ErrorMessage = "CommentId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CommentId must be a positive integer.")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ReactionType is required.")]
        [StringLength(10, ErrorMessage = "ReactionType cannot exceed 10 characters.")]
        [RegularExpression("^(like|love|haha|wow|sad|angry|care)$",
            ErrorMessage = "ReactionType must be one of: like, love, haha, wow, sad, angry, care.")]
        public string ReactionType { get; set; } = string.Empty;
    }
}