using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.UserSession.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.UserSession.Queries
{
    public class GetActiveSessionsQuery
    {
        private const string GetActiveSessionsSql = @"
            EXEC [dbo].[sp_GetUserActiveSessions] 
                @UserId";

        public static async Task<OperationResult<GetActiveSessionsResponseDto>> ExecuteAsync(
            GetActiveSessionsRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("GetActiveSessionsQuery received null data");
                return OperationResult<GetActiveSessionsResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing get active sessions for UserId: {UserId}", dto.UserId);

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
                logger.LogError(ex, "Database connection failed during get active sessions for UserId {UserId}",
                    dto.UserId);
                return OperationResult<GetActiveSessionsResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get active sessions for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<GetActiveSessionsResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get active sessions for UserId {UserId}",
                    dto.UserId);
                return OperationResult<GetActiveSessionsResponseDto>.Failure("Get active sessions failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, GetActiveSessionsRequestDto dto)
        {
            var command = new SqlCommand(GetActiveSessionsSql, connection);
            command.AddParameter("@UserId", dto.UserId);
            return command;
        }

        private static async Task<OperationResult<GetActiveSessionsResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            var response = new GetActiveSessionsResponseDto();
            var sessions = new List<ActiveSessionDto>();

            while (await reader.ReadAsync())
            {
                var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
                
                if (status != "SUCCESS")
                {
                    var errorMessage = reader.GetValueOrDefault<string>("ErrorMessage") ?? "Get active sessions failed";
                    var errorNumber = reader.GetValueOrDefault<int?>("ErrorNumber");
                    logger.LogWarning("Get active sessions failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                        userId, errorMessage, errorNumber);
                    return OperationResult<GetActiveSessionsResponseDto>.Failure(errorMessage);
                }

                sessions.Add(UserSessionMapper.MapActiveSessionFromReader(reader));

                
            }

            response.Sessions = sessions;

            logger.LogInformation("Active sessions retrieved successfully - UserId: {UserId}, Count: {Count}",
                userId, sessions.Count);

            return OperationResult<GetActiveSessionsResponseDto>.Success(response, "Active sessions retrieved successfully");
        }
    }
}