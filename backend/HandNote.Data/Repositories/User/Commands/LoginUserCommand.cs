using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Results;
using HandNote.Data.DTOs.User;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using HandNote.Data.Helpers;

namespace HandNote.Data.Repositories.User.Commands
{
    public class LoginUserCommand
    {
        private const string CreateUserSql = @"
            EXEC [dbo].[sp_LoginUser] @Email, @Password";

        public static async Task<OperationResult<UserAuthenticationData>> ExecuteAsync(
            UserLoginDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("LoginUserCommand received null registration data");
                return OperationResult<UserAuthenticationData>.Failure("Registration data is required");
            }

            logger.LogInformation("Executing user Login for Email: {Email}", dto.Email);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.Email);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during user login for {Email}", dto.Email);
                return OperationResult<UserAuthenticationData>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during user login for {Email}. Error: {Error}",
                    dto.Email, ex.Message);
                return OperationResult<UserAuthenticationData>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during user login for {Email}", dto.Email);
                return OperationResult<UserAuthenticationData>.Failure("User login failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, UserLoginDto dto)
        {
            var command = new SqlCommand(CreateUserSql, connection);
            command.AddParameter("@Email", dto.Email);
            command.AddParameter("@Password", dto.Password);
            command.AddParameter("@Provider", null);
            return command;
        }



        private static async Task<OperationResult<UserAuthenticationData>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            string Email)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from login procedure for {Email}", Email);
                return OperationResult<UserAuthenticationData>.Failure("Login procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status");
            var message = reader.GetValueOrDefault<string>("Message") ?? "Login completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Login failed for {Email}: {Message} (Error: {ErrorNumber})",
                    Email, message, errorNumber);
                return OperationResult<UserAuthenticationData>.Failure(message);
            }
            else
            {
                var user = UserMapper.MapUserFromReader(reader);
                var session = UserMapper.MapUserSessionFromReader(reader);
                var accessToken = TokenHandler.GenerateAccessTokenAsync(user, "Jwt:SigningKey");

                var authData = new UserAuthenticationData(user, session, accessToken);

                logger.LogInformation("User Login successfully - UserId: {UserId}, SessionId: {SessionId}",
                    user.UserId, session.SessionId);

                return OperationResult<UserAuthenticationData>.Success(authData, "User logined successfully");
            }
        }

    }
}