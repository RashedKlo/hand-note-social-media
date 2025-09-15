using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Friendship.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Friendship.Commands
{
    public class UpdateFriendCommand
    {
        private const string UpdateFriendSql = @"
            EXEC [dbo].[sp_UpdateFriendStatus] 
                @FriendshipId, @UserId, @NewStatus";

        public static async Task<OperationResult<FriendshipUpdateResponseDto>> ExecuteAsync(
            FriendshipUpdateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("UpdateFriendCommand received null friendship data");
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship data is required");
            }

            logger.LogInformation("Executing friendship status update for FriendshipId: {FriendshipId}, UserId: {UserId}, NewStatus: {NewStatus}",
                dto.FriendshipId, dto.UserId, dto.NewStatus);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.FriendshipId, dto.UserId, dto.NewStatus);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during friendship status update for FriendshipId {FriendshipId}, UserId {UserId}",
                    dto.FriendshipId, dto.UserId);
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during friendship status update for FriendshipId {FriendshipId}, UserId {UserId}. Error: {Error}",
                    dto.FriendshipId, dto.UserId, ex.Message);
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during friendship status update for FriendshipId {FriendshipId}, UserId {UserId}",
                    dto.FriendshipId, dto.UserId);
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship status update failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, FriendshipUpdateRequestDto dto)
        {
            var command = new SqlCommand(UpdateFriendSql, connection);
            command.AddParameter("@FriendshipId", dto.FriendshipId);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@NewStatus", dto.NewStatus);
            return command;
        }

        private static async Task<OperationResult<FriendshipUpdateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int friendshipId,
            int userId,
            string newStatus)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from friendship status update procedure for FriendshipId {FriendshipId}, UserId {UserId}",
                    friendshipId, userId);
                return OperationResult<FriendshipUpdateResponseDto>.Failure("Friendship status update procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Friendship status update completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Friendship status update failed for FriendshipId {FriendshipId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    friendshipId, userId, message, errorNumber);
                return OperationResult<FriendshipUpdateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = FriendshipMapper.MapUpdateResponseFromReader(reader);

                    logger.LogInformation("Friendship status updated successfully - FriendshipId: {FriendshipId}, UserId: {UserId}, NewStatus: {NewStatus}",
                        friendshipId, userId, newStatus);

                    return OperationResult<FriendshipUpdateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping friendship status update result for FriendshipId {FriendshipId}, UserId {UserId}",
                        friendshipId, userId);
                    return OperationResult<FriendshipUpdateResponseDto>.Failure("Failed to process friendship status update result");
                }
            }
        }
    }
}
