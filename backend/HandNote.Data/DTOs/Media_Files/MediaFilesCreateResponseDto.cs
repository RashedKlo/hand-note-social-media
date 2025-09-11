using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Media_Files
{
    public class MediaFilesCreateResponseDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Records inserted must be positive")]
        public int RecordsInserted { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total file paths must be positive")]
        public int TotalFilePaths { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


    }
}