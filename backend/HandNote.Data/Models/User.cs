using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandNote.Data.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(320)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [StringLength(255)]
        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(20)]
        [Column("gender_preference")]
        public string? GenderPreference { get; set; }

        [StringLength(500)]
        [Column("about_me")]
        public string? AboutMe { get; set; }

        [Required]
        [Column("city_id")]
        public int CityId { get; set; }

        [StringLength(200)]
        [Column("works_at")]
        public string? WorksAt { get; set; }

        [StringLength(200)]
        [Column("studied_at")]
        public string? StudiedAt { get; set; }

        [Required]
        [StringLength(20)]
        [Column("relationship_status")]
        public string RelationshipStatus { get; set; } = "not_specified";

        [StringLength(500)]
        [Column("profile_photo_url")]
        public string? ProfilePhotoUrl { get; set; }

        [Column("is_verified")]
        public bool IsVerified { get; set; } = false;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("profile_completed")]
        public bool ProfileCompleted { get; set; } = false;

        [Column("email_confirmed")]
        public bool EmailConfirmed { get; set; } = false;

        [Column("mobile_confirmed")]
        public bool MobileConfirmed { get; set; } = false;

        [Required]
        [StringLength(10)]
        [Column("account_privacy")]
        public string AccountPrivacy { get; set; } = "public";

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("last_updated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}