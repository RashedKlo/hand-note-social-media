using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Post.Delete
{
    public class PostDeleteResponseDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int DeletedRows { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}