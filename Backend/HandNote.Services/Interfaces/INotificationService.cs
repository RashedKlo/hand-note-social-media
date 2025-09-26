
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Creates a new notification
        /// </summary>
        /// <param name="dto">Request containing notification details</param>
        /// <returns>Created notification details or error information</returns>
        Task<OperationResult<NotificationCreateResponseDto>> CreateNotificationAsync(NotificationCreateRequestDto dto);

        /// <summary>
        /// Retrieves user notifications with pagination and filtering
        /// </summary>
        /// <param name="dto">Request containing user ID and pagination details</param>
        /// <returns>List of notifications or error information</returns>
        Task<OperationResult<List<NotificationsGetResponseDto>>> GetUserNotificationsAsync(NotificationsGetRequestDto dto);

        /// <summary>
        /// Marks a specific notification as read
        /// </summary>
        /// <param name="dto">Request containing notification ID and user ID</param>
        /// <returns>Update result or error information</returns>
        Task<OperationResult<NotificationMarkAsReadResponseDto>> MarkNotificationAsReadAsync(NotificationMarkAsReadRequestDto dto);

        /// <summary>
        /// Gets the count of unread notifications for a user
        /// </summary>
        /// <param name="dto">Request containing user ID</param>
        /// <returns>Unread notification count or error information</returns>
        Task<OperationResult<int>> GetUnreadNotificationCountAsync(int UserId);

    }
}