using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Update
{
    // Comment Update Response DTO
    public class CommentUpdateResponseDto
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public required string Content { get; set; }

        public string? MediaFile { get; set; }

        public bool? IsEdited { get; set; }

        public DateTime? EditedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}