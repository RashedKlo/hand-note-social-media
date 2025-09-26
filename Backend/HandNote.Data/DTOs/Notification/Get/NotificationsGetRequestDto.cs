using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Notification.Get
{
    /// <summary>
    /// Request DTO for sp_GetUserNotifications stored procedure
    /// </summary>
    public class NotificationsGetRequestDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 20;

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
        public int PageNumber { get; set; } = 1;

        public bool IncludeRead { get; set; } = true;
    }
}