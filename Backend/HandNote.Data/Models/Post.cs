using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.Models
{
    [Table("posts")]
    public class Post
    {
        [Key]
        [Column("post_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column("content")]
        [StringLength(1200)]
        public string? Content { get; set; }

        [Required]
        [Column("post_type")]
        [StringLength(20)]
        [RegularExpression("^(text|media|shared)$")]
        public string PostType { get; set; } = "text";

        [Column("shared_post_id")]
        [ForeignKey(nameof(Post))]
        public int? SharedPostId { get; set; }

        [Column("has_media")]
        public bool HasMedia { get; set; } = false;

        [Column("is_edited")]
        public bool IsEdited { get; set; } = false;

        [Column("edited_at")]
        public DateTime? EditedAt { get; set; }

        [Column("views_count")]
        public int ViewsCount { get; set; } = 0;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("allows_comments")]
        public bool AllowsComments { get; set; } = true;

        [Column("allows_shares")]
        public bool AllowsShares { get; set; } = true;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

    }
}