using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Post.Queries
{
    public class PostExistenceResponseDto
    {
        public int PostId { get; set; }
        public bool Exists { get; set; } = true;
        public int OwnerUserId { get; set; }
    }
}