using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Notification.Create
{
    /// <summary>
    /// Request DTO for sp_CreateNotification stored procedure
    /// </summary>
    public class NotificationCreateRequestDto
    {
        [Required(ErrorMessage = "RecipientId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RecipientId must be a positive integer.")]
        public int RecipientId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ActorId must be a positive integer if provided.")]
        public int? ActorId { get; set; }

        [Required(ErrorMessage = "NotificationType is required.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "NotificationType must be between 1 and 20 characters.")]
        public string NotificationType { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "TargetType cannot exceed 20 characters.")]
        [RegularExpression("^(post|comment|user|friendship)$",
            ErrorMessage = "TargetType must be one of: post, comment, user, friendship.")]
        public string? TargetType { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "TargetId must be a positive integer if provided.")]
        public long? TargetId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Message cannot exceed 200 characters.")]
        public string? Message { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }

}