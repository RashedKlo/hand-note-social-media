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
        [Range(0, int.MaxValue, ErrorMessage = "Records updated must be non-negative")]
        public int RecordsUpdated { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total media files must be positive")]
        public int TotalMediaFiles { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Updated media file IDs are required")]
        [MinLength(1, ErrorMessage = "At least one media file ID is required")]
        public required List<int> MediaFilesID { get; set; }
    }
}