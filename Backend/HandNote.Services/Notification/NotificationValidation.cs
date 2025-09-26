using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Notification;
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Notification
{
    public class NotificationValidation
    {
        public static OperationResult ValidateNotificationCreation(NotificationCreateRequestDto dto, ILogger logger)
        {
            logger.LogDebug("Starting notification creation validation for RecipientId: {RecipientId}, NotificationType: {NotificationType}",
                dto.RecipientId, dto.NotificationType);

            // Business rule: System notifications shouldn't have actors
            if ((dto.NotificationType == "system" || dto.NotificationType == "security" || dto.NotificationType == "memory")
                && dto.ActorId.HasValue)
            {
                logger.LogWarning("System notification type {NotificationType} should not have ActorId, but ActorId {ActorId} was provided",
                    dto.NotificationType, dto.ActorId);
                return OperationResult.Failure("System notifications cannot have an actor");
            }

            // Business rule: Target consistency validation
            if ((dto.TargetType != null && dto.TargetId == null) || (dto.TargetType == null && dto.TargetId != null))
            {
                logger.LogWarning("Target consistency validation failed - TargetType: {TargetType}, TargetId: {TargetId}",
                    dto.TargetType, dto.TargetId);
                return OperationResult.Failure("TargetType and TargetId must both be provided or both be null");
            }

            // Business rule: Future expiration validation
            if (dto.ExpiresAt.HasValue && dto.ExpiresAt <= DateTime.UtcNow)
            {
                logger.LogWarning("Notification expiration date {ExpiresAt} is not in the future", dto.ExpiresAt);
                return OperationResult.Failure("Expiration date must be in the future");
            }

            logger.LogDebug("Notification creation validation passed for RecipientId: {RecipientId}, NotificationType: {NotificationType}",
                dto.RecipientId, dto.NotificationType);

            return OperationResult.Success();
        }

        public static OperationResult ValidateNotificationReadAccess(NotificationMarkAsReadRequestDto dto, ILogger logger)
        {
            logger.LogDebug("Starting notification read access validation for NotificationId: {NotificationId}, UserId: {UserId}",
                dto.NotificationId, dto.UserId);

            // Additional validation logic can be added here for business rules
            // For example, checking if notification is already expired, etc.

            logger.LogDebug("Notification read access validation passed for NotificationId: {NotificationId}, UserId: {UserId}",
                dto.NotificationId, dto.UserId);

            return OperationResult.Success();
        }

        public static OperationResult ValidateNotificationAccess(NotificationsGetRequestDto dto, ILogger logger)
        {
            logger.LogDebug("Starting notification access validation for UserId: {UserId}, PageSize: {PageSize}, PageNumber: {PageNumber}",
                dto.UserId, dto.PageSize, dto.PageNumber);

            // Additional validation logic can be added here for business rules
            // For example, rate limiting, access control, etc.

            logger.LogDebug("Notification access validation passed for UserId: {UserId}",
                dto.UserId);

            return OperationResult.Success();
        }

        public static OperationResult ValidateUnreadCountAccessAsync(int UserId, ILogger logger)
        {
            logger.LogDebug("Starting unread count access validation for UserId: {UserId}", UserId);

            // Additional validation logic can be added here for business rules
            // For example, rate limiting, access control, etc.

            logger.LogDebug("Unread count access validation passed for UserId: {UserId}", UserId);

            return OperationResult.Success();
        }
    }
}