using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using HandNote.Data.Settings;
using HandNote.Data.DTOs.User;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Models;
using HandNote.Data.Results;
using System.Linq.Expressions;
using HandNote.Data.Helpers;

namespace HandNote.Data.Repositories.User.Commands
{
    public static class CreateUserCommand
    {
        private const string CreateUserSql = @"
            EXEC [dbo].[sp_RegisterUser] 
                @UserName, @Email, @Password, @FirstName, @LastName, @CityId, @Provider";

        public static async Task<OperationResult<UserAuthenticationData>> ExecuteAsync(
            UserRegistrationDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreateUserCommand received null registration data");
                return OperationResult<UserAuthenticationData>.Failure("Registration data is required");
            }

            logger.LogInformation("Executing user creation for username: {Username}", dto.UserName);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserName);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during user creation for {Username}", dto.UserName);
                return OperationResult<UserAuthenticationData>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during user creation for {Username}. Error: {Error}",
                    dto.UserName, ex.Message);
                return OperationResult<UserAuthenticationData>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during user creation for {Username}", dto.UserName);
                return OperationResult<UserAuthenticationData>.Failure("User creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, UserRegistrationDto dto)
        {
            var command = new SqlCommand(CreateUserSql, connection);
            // Use parameters to prevent SQL injection and handle null values properly
            command.AddParameter("@UserName", dto.UserName);
            command.AddParameter("@Email", dto.Email);
            command.AddParameter("@Password", dto.Password);
            command.AddParameter("@FirstName", dto.FirstName);
            command.AddParameter("@LastName", dto.LastName);
            command.AddParameter("@CityId", dto.CityId);
            command.AddParameter("@Provider", null);
            return command;
        }


        private static async Task<OperationResult<UserAuthenticationData>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            string userName)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from registration procedure for {Username}", userName);
                return OperationResult<UserAuthenticationData>.Failure("Registration procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status");
            var message = reader.GetValueOrDefault<string>("Message") ?? "Registration completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Registration failed for {Username}: {Message} (Error: {ErrorNumber})",
                    userName, message, errorNumber);
                return OperationResult<UserAuthenticationData>.Failure(message);
            }
            else
            {
                var user = UserMapper.MapUserFromReader(reader);
                var session = UserMapper.MapUserSessionFromReader(reader);
                var accessToken = TokenHandler.GenerateAccessTokenAsync(user, "Jwt:SigningKey");

                var authData = new UserAuthenticationData(user, session, accessToken);

                logger.LogInformation("User created successfully - UserId: {UserId}, SessionId: {SessionId}",
                    user.UserId, session.SessionId);

                return OperationResult<UserAuthenticationData>.Success(authData, "User created successfully");
            }
        }

    }


}