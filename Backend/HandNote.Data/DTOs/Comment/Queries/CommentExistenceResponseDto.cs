using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Comment.Queries
{
    public class CommentExistenceResponseDto
    {
        public int CommentId { get; set; }

        public bool? CommentExists { get; set; }

        public bool? IsDeleted { get; set; }

    }
}