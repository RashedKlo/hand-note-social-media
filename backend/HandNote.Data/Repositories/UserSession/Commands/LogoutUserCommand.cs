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
    public class LogoutUserCommand
    {
        private const string LogoutUserSql = @"
            EXEC [dbo].[sp_LogoutUser] 
                @Email, @SessionID";

        public static async Task<OperationResult<LogoutUserResponseDto>> ExecuteAsync(
            LogoutUserRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("LogoutUserCommand received null data");
                return OperationResult<LogoutUserResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing logout for Email: {Email}, SessionId: {SessionId}",
                dto.Email, dto.SessionId);

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
                logger.LogError(ex, "Database connection failed during logout for Email {Email}",
                    dto.Email);
                return OperationResult<LogoutUserResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during logout for Email {Email}. Error: {Error}",
                    dto.Email, ex.Message);
                return OperationResult<LogoutUserResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during logout for Email {Email}",
                    dto.Email);
                return OperationResult<LogoutUserResponseDto>.Failure("Logout failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, LogoutUserRequestDto dto)
        {
            var command = new SqlCommand(LogoutUserSql, connection);
            command.AddParameter("@Email", dto.Email);
            command.AddParameter("@SessionID", dto.SessionId);
            return command;
        }

        private static async Task<OperationResult<LogoutUserResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            string email)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from logout procedure for Email {Email}", email);
                return OperationResult<LogoutUserResponseDto>.Failure("Logout procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Logout completed";

            var responseDto = UserSessionMapper.MapLogoutUserResponseFromReader(reader);

            if (status != "SUCCESS")
            {
                logger.LogWarning("Logout failed for Email {Email}: {Message} (Error: {ErrorNumber})",
                    email, message, reader.GetValueOrDefault<int>("ErrorNumber"));
                return OperationResult<LogoutUserResponseDto>.Failure(message);
            }

            logger.LogInformation("User logged out successfully - Email: {Email}, UserId: {UserId}",
                email, responseDto.UserId);

            return OperationResult<LogoutUserResponseDto>.Success(responseDto, message);
        }
    }
}