using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Notification
{
    public sealed class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<NotificationCreateResponseDto>> CreateNotificationAsync(NotificationCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Notification creation attempted with null data");
                return OperationResult<NotificationCreateResponseDto>.Failure("Notification data is required");
            }

            _logger.LogInformation("Processing notification creation for RecipientId: {RecipientId}, NotificationType: {NotificationType}, Title: {Title}",
                dto.RecipientId, dto.NotificationType, dto.Title);

            var validationResult = NotificationValidation.ValidateNotificationCreation(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Notification creation validation failed for RecipientId {RecipientId}: {Error}",
                    dto.RecipientId, validationResult.Message);
                return OperationResult<NotificationCreateResponseDto>.Failure(validationResult.Message);
            }

            var createResult = await _notificationRepository.CreateNotificationAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Notification creation failed for RecipientId {RecipientId}: {Message}",
                    dto.RecipientId, createResult.Message);
                return OperationResult<NotificationCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Notification created successfully - NotificationId: {NotificationId}, RecipientId: {RecipientId}, NotificationType: {NotificationType}, Title: {Title}",
                createResult.Data?.NotificationId, dto.RecipientId, dto.NotificationType, dto.Title);

            return OperationResult<NotificationCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<List<NotificationsGetResponseDto>>> GetUserNotificationsAsync(NotificationsGetRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Get user notifications attempted with null data");
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Notification data is required");
            }

            _logger.LogInformation("Processing get user notifications for UserId: {UserId}, PageSize: {PageSize}, PageNumber: {PageNumber}, IncludeRead: {IncludeRead}",
                dto.UserId, dto.PageSize, dto.PageNumber, dto.IncludeRead);

            var validationResult = NotificationValidation.ValidateNotificationAccess(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Get user notifications validation failed for UserId {UserId}: {Error}",
                    dto.UserId, validationResult.Message);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure(validationResult.Message);
            }

            var getResult = await _notificationRepository.GetUserNotificationsAsync(dto);

            if (!getResult.IsSuccess)
            {
                _logger.LogWarning("Get user notifications failed for UserId {UserId}: {Message}",
                    dto.UserId, getResult.Message);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure(getResult.Message);
            }

            _logger.LogInformation("User notifications retrieved successfully - UserId: {UserId}, Notifications Count: {NotificationCount}",
                dto.UserId, getResult.Data?.Count ?? 0);

            return OperationResult<List<NotificationsGetResponseDto>>.Success(getResult.Data!, getResult.Message);
        }

        public async Task<OperationResult<NotificationMarkAsReadResponseDto>> MarkNotificationAsReadAsync(NotificationMarkAsReadRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Mark notification as read attempted with null data");
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Notification data is required");
            }

            _logger.LogInformation("Processing mark notification as read for NotificationId: {NotificationId}, UserId: {UserId}",
                dto.NotificationId, dto.UserId);

            var validationResult = NotificationValidation.ValidateNotificationReadAccess(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Mark notification as read validation failed for NotificationId {NotificationId}, UserId {UserId}: {Error}",
                    dto.NotificationId, dto.UserId, validationResult.Message);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure(validationResult.Message);
            }

            var markReadResult = await _notificationRepository.MarkNotificationAsReadAsync(dto);

            if (!markReadResult.IsSuccess)
            {
                _logger.LogWarning("Mark notification as read failed for NotificationId {NotificationId}, UserId {UserId}: {Message}",
                    dto.NotificationId, dto.UserId, markReadResult.Message);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure(markReadResult.Message);
            }

            _logger.LogInformation("Notification marked as read successfully - NotificationId: {NotificationId}, UserId: {UserId}, WasUpdated: {WasUpdated}",
                dto.NotificationId, dto.UserId, markReadResult.Data?.WasUpdated);

            return OperationResult<NotificationMarkAsReadResponseDto>.Success(markReadResult.Data!, markReadResult.Message);
        }

        public async Task<OperationResult<int>> GetUnreadNotificationCountAsync(int UserId)
        {
            _logger.LogInformation("Processing get unread notification count for UserId: {UserId}", UserId);

            var validationResult = NotificationValidation.ValidateUnreadCountAccessAsync(UserId, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Get unread notification count validation failed for UserId {UserId}: {Error}",
                    UserId, validationResult.Message);
                return OperationResult<int>.Failure(validationResult.Message);
            }

            var countResult = await _notificationRepository.GetUnreadNotificationCountAsync(UserId);

            if (!countResult.IsSuccess)
            {
                _logger.LogWarning("Get unread notification count failed for UserId {UserId}: {Message}",
                    UserId, countResult.Message);
                return OperationResult<int>.Failure(countResult.Message);
            }

            _logger.LogInformation("Unread notification count retrieved successfully - UserId: {UserId}, UnreadCount: {UnreadCount}",
                UserId, countResult);

            return OperationResult<int>.Success(countResult.Data!, countResult.Message);
        }
    }
}