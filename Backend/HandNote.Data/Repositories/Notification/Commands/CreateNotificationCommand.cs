
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Notification.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Notification.Commands
{
    public class CreateNotificationCommand
    {
        private const string CreateNotificationSql = @"
            EXEC [dbo].[sp_CreateNotification] 
                @RecipientId, @ActorId, @NotificationType, @TargetType, @TargetId, @Title, @Message, @ExpiresAt";

        public static async Task<OperationResult<NotificationCreateResponseDto>> ExecuteAsync(
            NotificationCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreateNotificationCommand received null notification data");
                return OperationResult<NotificationCreateResponseDto>.Failure("Notification data is required");
            }

            logger.LogInformation("Executing notification creation for RecipientId: {RecipientId}, NotificationType: {NotificationType}, Title: {Title}",
                dto.RecipientId, dto.NotificationType, dto.Title);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.RecipientId, dto.NotificationType);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during notification creation for RecipientId {RecipientId}",
                    dto.RecipientId);
                return OperationResult<NotificationCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during notification creation for RecipientId {RecipientId}. Error: {Error}",
                    dto.RecipientId, ex.Message);
                return OperationResult<NotificationCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during notification creation for RecipientId {RecipientId}",
                    dto.RecipientId);
                return OperationResult<NotificationCreateResponseDto>.Failure("Notification creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, NotificationCreateRequestDto dto)
        {
            var command = new SqlCommand(CreateNotificationSql, connection);
            command.AddParameter("@RecipientId", dto.RecipientId);
            command.AddParameter("@ActorId", dto.ActorId.HasValue ? dto.ActorId.Value : (object)DBNull.Value);
            command.AddParameter("@NotificationType", dto.NotificationType);
            command.AddParameter("@TargetType", dto.TargetType ?? (object)DBNull.Value);
            command.AddParameter("@TargetId", dto.TargetId.HasValue ? dto.TargetId.Value : (object)DBNull.Value);
            command.AddParameter("@Title", dto.Title);
            command.AddParameter("@Message", dto.Message ?? (object)DBNull.Value);
            command.AddParameter("@ExpiresAt", dto.ExpiresAt.HasValue ? dto.ExpiresAt.Value : (object)DBNull.Value);
            return command;
        }

        private static async Task<OperationResult<NotificationCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int recipientId,
            string notificationType)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from notification creation procedure for RecipientId {RecipientId}",
                    recipientId);
                return OperationResult<NotificationCreateResponseDto>.Failure("Notification creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Notification creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Notification creation failed for RecipientId {RecipientId}: {Message} (Error: {ErrorNumber})",
                    recipientId, message, errorNumber);
                return OperationResult<NotificationCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = NotificationMapper.MapCreateResponseFromReader(reader);

                    logger.LogInformation("Notification created successfully - NotificationId: {NotificationId}, RecipientId: {RecipientId}, NotificationType: {NotificationType}",
                        responseDto.NotificationId, recipientId, notificationType);

                    return OperationResult<NotificationCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping notification creation result for RecipientId {RecipientId}",
                        recipientId);
                    return OperationResult<NotificationCreateResponseDto>.Failure("Failed to process notification creation result");
                }
            }
        }


    }
}