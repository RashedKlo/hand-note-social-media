using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.Models
{

    [Table("user_sessions")]
    public class UserSession
    {
        [Key]
        [Column("session_id")]
        public int SessionId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;


        [StringLength(255)]
        [Column("provider")]
        public string? Provider { get; set; }

        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
