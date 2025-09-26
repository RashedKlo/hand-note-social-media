using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using HandNote.Data.Settings;
using HandNote.Data.DTOs.User;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Models;
using HandNote.Data.Results;
using HandNote.Data.Helpers;

namespace HandNote.Data.Repositories.User.Commands
{
    public class AuthenticateGoogleCommand
    {
        private const string AuthenticateGoogleSql = @"
            EXEC [dbo].[sp_AuthenticateGoogle] 
                @Email, @FirstName, @LastName, @ProfilePhotoUrl";

        public static async Task<OperationResult<GoogleUserAuthenticationData>> ExecuteAsync(
           string Email,
            ILogger logger,
            JwtSettings jwtSettings)
        {

            logger.LogInformation("Executing Google authentication for email: {Email}", Email);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, Email);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, Email, jwtSettings);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during Google authentication for {Email}", Email);
                return OperationResult<GoogleUserAuthenticationData>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during Google authentication for {Email}. Error: {Error}",
                    Email, ex.Message);
                return OperationResult<GoogleUserAuthenticationData>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during Google authentication for {Email}, Error: {Error}",
                    Email, ex.Message);
                return OperationResult<GoogleUserAuthenticationData>.Failure("Google authentication failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, string Email)
        {
            var command = new SqlCommand(AuthenticateGoogleSql, connection);

            // Use parameters to prevent SQL injection and handle null values properly
            command.AddParameter("@Email", Email);
            // command.AddParameter("@FirstName", null);
            // command.AddParameter("@LastName", null);
            // command.AddParameter("@ProfilePhotoUrl", null);
            return command;
        }

        private static async Task<OperationResult<GoogleUserAuthenticationData>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
           string Email,
            JwtSettings jwtSettings)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from Google authentication procedure for {Email}", Email);
                return OperationResult<GoogleUserAuthenticationData>.Failure("Google authentication procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status");
            var message = reader.GetValueOrDefault<string>("Message") ?? "Google authentication completed";
            var isExistingUser = reader.GetValueOrDefault<bool?>("IsExistingUser") ?? false;

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int?>("ErrorNumber");
                logger.LogWarning("Google authentication failed for {Email}: {Message} (Error: {ErrorNumber})",
                    Email, message, errorNumber);
                return OperationResult<GoogleUserAuthenticationData>.Failure(message);
            }
            else
            {
                var user = UserMapper.MapUserFromReader(reader);
                var session = UserMapper.MapUserSessionFromReader(reader);
                var accessToken = TokenHandler.GenerateAccessTokenAsync(user, jwtSettings.SigningKey);

                var authData = new GoogleUserAuthenticationData(user, session, accessToken, !isExistingUser);

                logger.LogInformation("Google authentication successful - UserId: {UserId}, SessionId: {SessionId}, IsNewUser: {IsNewUser}",
                    user.UserId, session.SessionId, !isExistingUser);

                return OperationResult<GoogleUserAuthenticationData>.Success(authData,
                    isExistingUser ? "Google login successful" : "Google registration successful");
            }
        }
    }


}