
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Notification.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Notification.Commands
{
    public class MarkNotificationAsReadCommand
    {
        private const string MarkNotificationAsReadSql = @"
            EXEC [dbo].[sp_MarkNotificationAsRead] 
                @NotificationId, @UserId";

        public static async Task<OperationResult<NotificationMarkAsReadResponseDto>> ExecuteAsync(
            NotificationMarkAsReadRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("MarkNotificationAsReadCommand received null notification data");
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Notification data is required");
            }

            logger.LogInformation("Executing mark notification as read for NotificationId: {NotificationId}, UserId: {UserId}",
                dto.NotificationId, dto.UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.NotificationId, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during mark notification as read for NotificationId {NotificationId}, UserId {UserId}",
                    dto.NotificationId, dto.UserId);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during mark notification as read for NotificationId {NotificationId}, UserId {UserId}. Error: {Error}",
                    dto.NotificationId, dto.UserId, ex.Message);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during mark notification as read for NotificationId {NotificationId}, UserId {UserId}",
                    dto.NotificationId, dto.UserId);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Mark notification as read failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, NotificationMarkAsReadRequestDto dto)
        {
            var command = new SqlCommand(MarkNotificationAsReadSql, connection);
            command.AddParameter("@NotificationId", dto.NotificationId);
            command.AddParameter("@UserId", dto.UserId);
            return command;
        }

        private static async Task<OperationResult<NotificationMarkAsReadResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int notificationId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from mark notification as read procedure for NotificationId {NotificationId}, UserId {UserId}",
                    notificationId, userId);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Mark notification as read procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Mark notification as read completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Mark notification as read failed for NotificationId {NotificationId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    notificationId, userId, message, errorNumber);
                return OperationResult<NotificationMarkAsReadResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = NotificationMapper.MapMarkAsReadResponseFromReader(reader);

                    logger.LogInformation("Notification marked as read successfully - NotificationId: {NotificationId}, UserId: {UserId}, WasUpdated: {WasUpdated}",
                        notificationId, userId, responseDto.WasUpdated);

                    return OperationResult<NotificationMarkAsReadResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping mark notification as read result for NotificationId {NotificationId}, UserId {UserId}",
                        notificationId, userId);
                    return OperationResult<NotificationMarkAsReadResponseDto>.Failure("Failed to process mark notification as read result");
                }
            }
        }
    }
}