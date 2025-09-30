using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.UserSession.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.UserSession.Commands
{
    public class LogoutAllSessionsCommand
    {
        private const string LogoutAllSessionsSql = @"
            EXEC [dbo].[sp_LogoutUserAllSessions] 
                @Email";

        public static async Task<OperationResult<LogoutAllSessionsResponseDto>> ExecuteAsync(
            LogoutAllSessionsRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("LogoutAllSessionsCommand received null data");
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing logout all sessions for Email: {Email}", dto.Email);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.Email);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during logout all sessions for Email {Email}",
                    dto.Email);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during logout all sessions for Email {Email}. Error: {Error}",
                    dto.Email, ex.Message);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during logout all sessions for Email {Email}",
                    dto.Email);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Logout all sessions failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, LogoutAllSessionsRequestDto dto)
        {
            var command = new SqlCommand(LogoutAllSessionsSql, connection);
            command.AddParameter("@Email", dto.Email);
            return command;
        }

        private static async Task<OperationResult<LogoutAllSessionsResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            string email)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from logout all sessions procedure for Email {Email}", email);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Logout all sessions procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Logout all sessions completed";

            var responseDto = UserSessionMapper.MapLogoutAllSessionsResponseFromReader(reader);

            if (status != "SUCCESS")
            {
                logger.LogWarning("Logout all sessions failed for Email {Email}: {Message} (Error: {ErrorNumber})",
                    email, message, reader.GetValueOrDefault<int?>("ErrorNumber"));
                return OperationResult<LogoutAllSessionsResponseDto>.Failure(message);
            }

            logger.LogInformation("All sessions logged out successfully - Email: {Email}, Sessions: {Count}",
                email, responseDto.SessionsLoggedOut);

            return OperationResult<LogoutAllSessionsResponseDto>.Success(responseDto, message);
        }
    }
}