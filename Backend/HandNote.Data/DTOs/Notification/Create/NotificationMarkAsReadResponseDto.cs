using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Notification.Create
{
    /// <summary>
    /// Response DTO for sp_MarkNotificationAsRead stored procedure
    /// </summary>
    public class NotificationMarkAsReadResponseDto
    {
        /// <summary>
        /// ID of the notification
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "NotificationId must be a positive integer.")]
        public int NotificationId { get; set; }

        /// <summary>
        /// ID of the user
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        /// <summary>
        /// Number of records updated (NULL for errors)
        /// </summary>
        public int? UpdatedCount { get; set; }

        /// <summary>
        /// Whether the notification was actually updated (NULL for errors)
        /// </summary>
        public bool? WasUpdated { get; set; }

    }
}