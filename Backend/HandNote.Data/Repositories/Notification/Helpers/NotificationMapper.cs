
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.Notification.Helpers
{
    public class NotificationMapper
    {
        public static NotificationCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            return new NotificationCreateResponseDto
            {
                NotificationId = reader.GetValueOrDefault<int?>("NotificationId"),
                RecipientId = reader.GetValueOrDefault<int>("RecipientId"),
                ActorId = reader.GetValueOrDefault<int?>("ActorId"),
                NotificationType = reader.GetValueOrDefault<string>("NotificationType") ?? string.Empty,
                TargetType = reader.GetValueOrDefault<string>("TargetType"),
                TargetId = reader.GetValueOrDefault<long?>("TargetId"),
                Title = reader.GetValueOrDefault<string>("Title") ?? string.Empty,
                Message = reader.GetValueOrDefault<string>("Message"),
                IsRead = reader.GetValueOrDefault<bool?>("IsRead"),
                ReadAt = reader.GetValueOrDefault<DateTime?>("ReadAt"),
                IsSent = reader.GetValueOrDefault<bool?>("IsSent"),
                SentAt = reader.GetValueOrDefault<DateTime?>("SentAt"),
                ExpiresAt = reader.GetValueOrDefault<DateTime?>("ExpiresAt"),
                CreatedAt = reader.GetValueOrDefault<DateTime?>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
            };
        }
        public static NotificationMarkAsReadResponseDto MapMarkAsReadResponseFromReader(SqlDataReader reader)
        {
            return new NotificationMarkAsReadResponseDto
            {
                NotificationId = reader.GetValueOrDefault<int>("NotificationId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                UpdatedCount = reader.GetValueOrDefault<int?>("UpdatedCount"),
                WasUpdated = reader.GetValueOrDefault<bool?>("WasUpdated"),
            };
        }

        public static NotificationsGetResponseDto MapNotificationFromReader(SqlDataReader reader)
        {
            return new NotificationsGetResponseDto
            {
                NotificationId = reader.GetValueOrDefault<int?>("NotificationId"),
                RecipientId = reader.GetValueOrDefault<int?>("RecipientId"),
                ActorId = reader.GetValueOrDefault<int?>("ActorId"),
                ActorUsername = reader.GetValueOrDefault<string>("ActorUsername"),
                ActorFirstName = reader.GetValueOrDefault<string>("ActorFirstName"),
                ActorProfilePicture = reader.GetValueOrDefault<string>("ActorProfilePicture"),
                NotificationType = reader.GetValueOrDefault<string>("NotificationType"),
                TargetType = reader.GetValueOrDefault<string>("TargetType"),
                TargetId = reader.GetValueOrDefault<long?>("TargetId"),
                Title = reader.GetValueOrDefault<string>("Title"),
                Message = reader.GetValueOrDefault<string>("Message"),
                IsRead = reader.GetValueOrDefault<bool?>("IsRead"),
                ReadAt = reader.GetValueOrDefault<DateTime?>("ReadAt"),
                IsSent = reader.GetValueOrDefault<bool?>("IsSent"),
                SentAt = reader.GetValueOrDefault<DateTime?>("SentAt"),
                ExpiresAt = reader.GetValueOrDefault<DateTime?>("ExpiresAt"),
                CreatedAt = reader.GetValueOrDefault<DateTime?>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
            };
        }

    }
}