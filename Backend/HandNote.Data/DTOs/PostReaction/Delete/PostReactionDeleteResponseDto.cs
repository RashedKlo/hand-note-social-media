using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.PostReaction.Delete
{

    /// <summary>
    /// Response DTO for sp_RemovePostReaction stored procedure
    /// Maps exactly to the return structure of the remove reaction operation
    /// </summary>
    public class PostReactionDeleteResponseDto
    {
        /// <summary>
        /// Unique identifier for the removed reaction (NULL for errors)
        /// </summary>
        public int? ReactionId { get; set; }

        /// <summary>
        /// ID of the post the reaction was removed from
        /// </summary>

        public int PostId { get; set; }

        /// <summary>
        /// ID of the user who removed the reaction
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        /// The type of reaction that was removed (NULL for errors)
        /// </summary>
        public string? RemovedReactionType { get; set; }

        /// <summary>
        /// When the reaction was removed (NULL for errors)
        /// </summary>
        public DateTime? RemovedAt { get; set; }

    }
}