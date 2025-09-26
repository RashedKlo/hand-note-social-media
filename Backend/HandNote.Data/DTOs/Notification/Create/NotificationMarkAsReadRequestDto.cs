using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Notification.Create
{
    /// <summary>
    /// Request DTO for sp_MarkNotificationAsRead stored procedure
    /// </summary>
    public class NotificationMarkAsReadRequestDto
    {
        [Required(ErrorMessage = "NotificationId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "NotificationId must be a positive integer.")]
        public int NotificationId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }
    }

}