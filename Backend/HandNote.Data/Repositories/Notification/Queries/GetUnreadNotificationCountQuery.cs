using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Notification.Queries
{
    public class GetUnreadNotificationCountQuery
    {
        private const string GetUnreadNotificationCountSql = @"
            EXEC [dbo].[sp_GetUnreadNotificationCount] 
                @UserId";

        public static async Task<OperationResult<int>> ExecuteAsync(
            int UserId,
            ILogger logger)
        {
            if (UserId <= 0)
            {
                logger.LogError("GetUnreadNotificationCountQuery received UserId less than 0");
                return OperationResult<int>.Failure("UserId is required");
            }

            logger.LogInformation("Executing get unread notification count for UserId: {UserId}", UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, UserId);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during get unread notification count for UserId {UserId}",
                    UserId);
                return OperationResult<int>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get unread notification count for UserId {UserId}. Error: {Error}",
                    UserId, ex.Message);
                return OperationResult<int>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get unread notification count for UserId {UserId}",
                    UserId);
                return OperationResult<int>.Failure("Get unread notification count failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int UserId)
        {
            var command = new SqlCommand(GetUnreadNotificationCountSql, connection);
            command.AddParameter("@UserId", UserId);
            return command;
        }

        private static async Task<OperationResult<int>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from get unread notification count procedure for UserId {UserId}",
                    userId);
                return OperationResult<int>.Failure("Get unread notification count procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Get unread notification count completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Get unread notification count failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<int>.Failure(message);
            }
            else
            {
                try
                {
                    int UnreadCount = reader.GetValueOrDefault<int>("UnreadCount");


                    logger.LogInformation("Get unread notification count completed successfully - UserId: {UserId}, UnreadCount: {UnreadCount}",
                        userId, UnreadCount);

                    return OperationResult<int>.Success(UnreadCount, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping get unread notification count result for UserId {UserId}", userId);
                    return OperationResult<int>.Failure("Failed to process get unread notification count result");
                }
            }
        }
    }
}