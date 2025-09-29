using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Notification.Create
{
    /// <summary>
    /// Response DTO for sp_CreateNotification stored procedure
    /// </summary>
    public class NotificationCreateResponseDto
    {
        /// <summary>
        /// Unique identifier for the notification (NULL for errors)
        /// </summary>
        public int? NotificationId { get; set; }

        /// <summary>
        /// ID of the notification recipient
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RecipientId must be a positive integer.")]
        public int RecipientId { get; set; }

        /// <summary>
        /// ID of the actor who triggered the notification (NULL for errors)
        /// </summary>
        public int? ActorId { get; set; }

        /// <summary>
        /// Type of notification
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "NotificationType cannot exceed 20 characters.")]
        public string NotificationType { get; set; } = string.Empty;

        /// <summary>
        /// Type of target entity (NULL for errors)
        /// </summary>
        [StringLength(20, ErrorMessage = "TargetType cannot exceed 20 characters.")]
        public string? TargetType { get; set; }

        /// <summary>
        /// ID of target entity (NULL for errors)
        /// </summary>
        public long? TargetId { get; set; }

        /// <summary>
        /// Notification title
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Notification message (NULL for errors)
        /// </summary>
        [StringLength(200, ErrorMessage = "Message cannot exceed 200 characters.")]
        public string? Message { get; set; }

        /// <summary>
        /// Whether the notification is read (NULL for errors)
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>
        /// When the notification was read (NULL for errors)
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// Whether the notification is sent (NULL for errors)
        /// </summary>
        public bool? IsSent { get; set; }

        /// <summary>
        /// When the notification was sent (NULL for errors)
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// When the notification expires (NULL for errors)
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// When the notification was created (NULL for errors)
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// When the notification was last updated (NULL for errors)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

    }

}