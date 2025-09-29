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
    public class UpdateUserPrivacySettingsCommand
    {
        private const string UpdatePrivacySettingsSql = @"
            EXEC [dbo].[sp_UpdateUserPrivacySettings] 
                @UserId, @DefaultPostAudience, @ProfileVisibility, 
                @FriendListVisibility, @FriendRequestFrom, @MessageFrom";

        public static async Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> ExecuteAsync(
            UserPrivacySettingsUpdateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("UpdateUserPrivacySettingsCommand received null data");
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing privacy settings update for UserId: {UserId}", dto.UserId);

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
                logger.LogError(ex, "Database connection failed during privacy settings update for UserId {UserId}",
                    dto.UserId);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during privacy settings update for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during privacy settings update for UserId {UserId}",
                    dto.UserId);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Privacy settings update failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, UserPrivacySettingsUpdateRequestDto dto)
        {
            var command = new SqlCommand(UpdatePrivacySettingsSql, connection);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@DefaultPostAudience", dto.DefaultPostAudience);
            command.AddParameter("@ProfileVisibility", dto.ProfileVisibility);
            command.AddParameter("@FriendListVisibility", dto.FriendListVisibility);
            command.AddParameter("@FriendRequestFrom", dto.FriendRequestFrom);
            command.AddParameter("@MessageFrom", dto.MessageFrom);
            return command;
        }

        private static async Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from privacy settings update procedure for UserId {UserId}",
                    userId);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Privacy settings update procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Privacy settings update completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Privacy settings update failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = UserPrivacySettingsMapper.MapUpdateResponseFromReader(reader);

                    logger.LogInformation("Privacy settings updated successfully - UserId: {UserId}", userId);

                    return OperationResult<UserPrivacySettingsUpdateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping privacy settings update result for UserId {UserId}",
                        userId);
                    return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Failed to process privacy settings update result");
                }
            }
        }
    }
}
