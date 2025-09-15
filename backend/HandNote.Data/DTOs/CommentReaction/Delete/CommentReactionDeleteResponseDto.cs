using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.CommentReaction.Delete
{
    /// <summary>
    /// Response DTO for sp_RemoveCommentReaction stored procedure
    /// </summary>
    public class CommentReactionDeleteResponseDto
    {
        /// <summary>
        /// Unique identifier for the removed reaction (NULL for errors)
        /// </summary>
        public int? ReactionId { get; set; }

        /// <summary>
        /// ID of the comment the reaction was removed from
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CommentId must be a positive integer.")]
        public int CommentId { get; set; }

        /// <summary>
        /// ID of the user who removed the reaction
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        /// <summary>
        /// The type of reaction that was removed (NULL for errors)
        /// </summary>
        [StringLength(10, ErrorMessage = "RemovedReactionType cannot exceed 10 characters.")]
        [RegularExpression("^(like|love|haha|wow|sad|angry|care)$",
            ErrorMessage = "RemovedReactionType must be one of: like, love, haha, wow, sad, angry, care.")]
        public string? RemovedReactionType { get; set; }

        /// <summary>
        /// When the reaction was removed (NULL for errors)
        /// </summary>
        public DateTime? RemovedAt { get; set; }
    }
}