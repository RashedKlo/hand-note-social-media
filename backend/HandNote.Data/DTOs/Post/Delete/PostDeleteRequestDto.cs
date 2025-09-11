using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Post.Delete
{
    public class PostDeleteRequestDto
    {
        [Required(ErrorMessage = "PostId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PostId must be a positive integer")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer")]
        public int UserId { get; set; }

    }
}