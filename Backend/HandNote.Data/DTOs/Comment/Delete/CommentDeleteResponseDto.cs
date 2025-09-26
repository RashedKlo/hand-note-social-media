using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Delete
{
    public class CommentDeleteResponseDto
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }

}