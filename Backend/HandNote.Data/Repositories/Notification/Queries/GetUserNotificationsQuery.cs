using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Notification.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Notification.Queries
{
    public class GetUserNotificationsQuery
    {
        private const string GetUserNotificationsSql = @"
            EXEC [dbo].[sp_GetUserNotifications] 
                @UserId, @PageSize, @PageNumber, @IncludeRead";

        public static async Task<OperationResult<List<NotificationsGetResponseDto>>> ExecuteAsync(
            NotificationsGetRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("GetUserNotificationsQuery received null notification data");
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Notification data is required");
            }

            logger.LogInformation("Executing get user notifications for UserId: {UserId}, PageSize: {PageSize}, PageNumber: {PageNumber}, IncludeRead: {IncludeRead}",
                dto.UserId, dto.PageSize, dto.PageNumber, dto.IncludeRead);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during get user notifications for UserId {UserId}",
                    dto.UserId);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get user notifications for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get user notifications for UserId {UserId}",
                    dto.UserId);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Get user notifications failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, NotificationsGetRequestDto dto)
        {
            var command = new SqlCommand(GetUserNotificationsSql, connection);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@PageSize", dto.PageSize);
            command.AddParameter("@PageNumber", dto.PageNumber);
            command.AddParameter("@IncludeRead", dto.IncludeRead);
            return command;
        }

        private static async Task<OperationResult<List<NotificationsGetResponseDto>>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            var notifications = new List<NotificationsGetResponseDto>();

            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from get user notifications procedure for UserId {UserId}",
                    userId);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Get user notifications procedure returned no result");
            }

            // Process the first row to check status
            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Get user notifications completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Get user notifications failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure(message);
            }

            try
            {
                // Process all rows for successful results
                do
                {
                    // Skip rows that don't have notification data (error rows)
                    if (reader.GetValueOrDefault<int?>("NotificationId").HasValue)
                    {
                        var notificationDto = NotificationMapper.MapNotificationFromReader(reader);
                        notifications.Add(notificationDto);
                    }
                } while (await reader.ReadAsync());

                logger.LogInformation("Get user notifications completed successfully - UserId: {UserId}, Notifications Count: {NotificationCount}",
                    userId, notifications.Count);

                return OperationResult<List<NotificationsGetResponseDto>>.Success(notifications,
                    $"Successfully retrieved {notifications.Count} notifications");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error mapping get user notifications result for UserId {UserId}", userId);
                return OperationResult<List<NotificationsGetResponseDto>>.Failure("Failed to process get user notifications result");
            }
        }


    }

}