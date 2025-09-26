
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.Notification.Commands;
using HandNote.Data.Repositories.Notification.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(ILogger<NotificationRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<NotificationCreateResponseDto>> CreateNotificationAsync(NotificationCreateRequestDto dto)
        {
            _logger.LogInformation("NotificationRepository.CreateNotificationAsync called for RecipientId: {RecipientId}, NotificationType: {NotificationType}",
                dto.RecipientId, dto.NotificationType);

            return await CreateNotificationCommand.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<List<NotificationsGetResponseDto>>> GetUserNotificationsAsync(NotificationsGetRequestDto dto)
        {
            _logger.LogInformation("NotificationRepository.GetUserNotificationsAsync called for UserId: {UserId}, PageSize: {PageSize}, PageNumber: {PageNumber}",
                dto.UserId, dto.PageSize, dto.PageNumber);

            return await GetUserNotificationsQuery.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<NotificationMarkAsReadResponseDto>> MarkNotificationAsReadAsync(NotificationMarkAsReadRequestDto dto)
        {
            _logger.LogInformation("NotificationRepository.MarkNotificationAsReadAsync called for NotificationId: {NotificationId}, UserId: {UserId}",
                dto.NotificationId, dto.UserId);

            return await MarkNotificationAsReadCommand.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<int>> GetUnreadNotificationCountAsync(int UserId)
        {
            _logger.LogInformation("NotificationRepository.GetUnreadNotificationCountAsync called for UserId: {UserId}",
                UserId);

            return await GetUnreadNotificationCountQuery.ExecuteAsync(UserId, _logger);
        }
    }
}