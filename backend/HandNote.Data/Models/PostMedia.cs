using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Models;

namespace HandNote.Data.Models
{
    [Table("post_media")]
    public class PostMedia
    {
        [Key]
        [Column("post_media_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostMediaId { get; set; }

        [Required]
        [Column("post_id")]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        [Required]
        [Column("media_id")]
        [ForeignKey(nameof(MediaFile))]
        public int MediaId { get; set; }

        [Column("media_order")]
        [Range(0, 10)]
        public int MediaOrder { get; set; } = 0;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
