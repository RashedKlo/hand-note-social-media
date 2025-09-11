using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.Models
{
    public class MediaFile
    {
        [Key]
        [Column("media_id")]
        public int MediaId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("file_path")]
        [StringLength(500)]
        public int FilePath { get; set; }

        [Required]
        [Column("created_at")]
        public int CreatedAt { get; set; }


    }
}