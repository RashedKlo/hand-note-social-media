using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.CommentReaction.Create
{/// <summary>
 /// Response DTO for sp_AddCommentReaction stored procedure
 /// </summary>
    public class CommentReactionCreateResponseDto
    {
        /// <summary>
        /// Unique identifier for the reaction (NULL for errors)
        /// </summary>
        public int? ReactionId { get; set; }

        /// <summary>
        /// ID of the comment being reacted to
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CommentId must be a positive integer.")]
        public int CommentId { get; set; }

        /// <summary>
        /// ID of the user who made the reaction
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        /// <summary>
        /// Type of reaction (NULL for errors)
        /// </summary>
        [StringLength(10, ErrorMessage = "ReactionType cannot exceed 10 characters.")]
        [RegularExpression("^(like|love|haha|wow|sad|angry|care)$",
            ErrorMessage = "ReactionType must be one of: like, love, haha, wow, sad, angry, care.")]
        public string? ReactionType { get; set; }

        /// <summary>
        /// Indicates if this reaction was updated from a previous reaction type (NULL for errors)
        /// </summary>
        public bool? IsUpdate { get; set; }

        /// <summary>
        /// When the reaction was created (NULL for errors)
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// When the reaction was last updated (NULL for errors)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}