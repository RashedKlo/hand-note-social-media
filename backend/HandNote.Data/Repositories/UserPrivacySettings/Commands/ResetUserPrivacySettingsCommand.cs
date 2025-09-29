// ResetUserPrivacySettingsCommand.cs
using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.UserPrivacySettings.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.UserPrivacySettings.Commands
{
    public class ResetUserPrivacySettingsCommand
    {
        private const string ResetPrivacySettingsSql = @"
            EXEC [dbo].[sp_ResetUserPrivacySettings] 
                @UserId";

        public static async Task<OperationResult<UserPrivacySettingsResetResponseDto>> ExecuteAsync(
            int UserId,
            ILogger logger)
        {
            if (UserId<=0)
            {
                logger.LogError("ResetUserPrivacySettingsCommand received null data");
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing privacy settings reset for UserId: {UserId}", UserId);

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
                logger.LogError(ex, "Database connection failed during privacy settings reset for UserId {UserId}",
                    UserId);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during privacy settings reset for UserId {UserId}. Error: {Error}",
                    UserId, ex.Message);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during privacy settings reset for UserId {UserId}",
                    UserId);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Privacy settings reset failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int UserId)
        {
            var command = new SqlCommand(ResetPrivacySettingsSql, connection);
            command.AddParameter("@UserId", UserId);
            return command;
        }

        private static async Task<OperationResult<UserPrivacySettingsResetResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from privacy settings reset procedure for UserId {UserId}",
                    userId);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Privacy settings reset procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Privacy settings reset completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Privacy settings reset failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = UserPrivacySettingsMapper.MapResetResponseFromReader(reader);

                    logger.LogInformation("Privacy settings reset successfully - UserId: {UserId}", userId);

                    return OperationResult<UserPrivacySettingsResetResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping privacy settings reset result for UserId {UserId}",
                        userId);
                    return OperationResult<UserPrivacySettingsResetResponseDto>.Failure("Failed to process privacy settings reset result");
                }
            }
        }
    }
}