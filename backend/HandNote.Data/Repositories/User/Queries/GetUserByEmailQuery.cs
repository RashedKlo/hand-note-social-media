using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.User.Queries
{
    public class GetUserByEmailQuery
    {

        private const string GetUserSql = @"
            EXEC [dbo].[sp_GetUserByEmail] @Email";

        public static async Task<OperationResult<Models.User>> ExecuteAsync(
           string Email,
            ILogger logger)
        {
            if (string.IsNullOrEmpty(Email))
            {
                logger.LogError("GetUserByEmailQuery received Empty Email data");
                return OperationResult<Models.User>.Failure(" Email is required");
            }

            logger.LogInformation("Executing getting user by Email: {Email}", Email);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, Email);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, Email);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during user getting user by {Email}", Email);
                return OperationResult<Models.User>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during getting user by {Email}. Error: {Error}",
                    Email, ex.Message);
                return OperationResult<Models.User>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during getting user by {Email}", Email);
                return OperationResult<Models.User>.Failure("Getting user failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, string Email)
        {
            var command = new SqlCommand(GetUserSql, connection);
            command.AddParameter("@Email", Email);
            return command;
        }



        private static async Task<OperationResult<Models.User>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            string Email)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from getting user by email procedure for {Email}", Email);
                return OperationResult<Models.User>.Failure("User is not found");
            }

            var status = reader.GetValueOrDefault<string>("Status");
            var message = reader.GetValueOrDefault<string>("Message") ?? "User is found";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Getting User failed for {Email}: {Message} (Error: {ErrorNumber})",
                    Email, message, errorNumber);
                return OperationResult<Models.User>.Failure(message);
            }
            else
            {
                var user = UserMapper.MapUserFromReader(reader);
                logger.LogInformation("Getting user by email successfully - UserId: {UserId}, Email: {Email}",
                    user.UserId, user.Email);

                return OperationResult<Models.User>.Success(user, "User is found successfully");
            }
        }
    }
}
