using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.PostReaction.Create
{
    /// <summary>
    /// Response DTO for sp_AddPostReaction stored procedure
    /// Maps exactly to the return structure of the add reaction operation
    /// </summary>
    public class PostReactionCreateResponseDto
    {
        /// <summary>
        /// Unique identifier for the reaction (NULL for errors)
        /// </summary>
        public int? ReactionId { get; set; }

        /// <summary>
        /// ID of the post being reacted to
        /// </summary>

        public int PostId { get; set; }

        /// <summary>
        /// ID of the user who made the reaction
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        /// Type of reaction (NULL for errors)
        /// </summary>
        public string? ReactionType { get; set; }


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