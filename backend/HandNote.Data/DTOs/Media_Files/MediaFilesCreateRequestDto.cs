using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Media_Files
{
    public class MediaFilesCreateRequestDto
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "File paths are required")]
        [MinLength(1, ErrorMessage = "At least one file path is required")]
        public List<string> FilePaths { get; set; } = new List<string>();
    }
}