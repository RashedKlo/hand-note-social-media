using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Message;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Friendship.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Message.Queries
{
    public class GetUserFriendsQuery
    {
        private const string GetUserFriendsSql = @"
            EXEC [dbo].[sp_GetUserFriends] 
                @UserId";

        public static async Task<OperationResult<List<UserFriendsGetResponseDto>>> ExecuteAsync(
            UserFriendsGetRequestDto dto,
            ILogger logger)
        {
            if (dto.UserId <= 0)
            {
                logger.LogError("GetUserFriendsQuery received invalid UserId: {UserId}", dto.UserId);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("UserId is required and must be a positive integer");
            }

            logger.LogInformation("Executing get user friends for UserId: {UserId}", dto.UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto.UserId);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during get user friends for UserId {UserId}",
                    dto.UserId);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get user friends for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get user friends for UserId {UserId}",
                    dto.UserId);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("Get user friends failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int userId)
        {
            var command = new SqlCommand(GetUserFriendsSql, connection);
            command.AddParameter("@UserId", userId);
            return command;
        }

        private static async Task<OperationResult<List<UserFriendsGetResponseDto>>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            var friends = new List<UserFriendsGetResponseDto>();

            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from get user friends procedure for UserId {UserId}",
                    userId);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("Get user friends procedure returned no result");
            }

            // Process the first row to check status
            var status = reader.GetValueOrDefault<string>("Status") ?? "ERROR";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Get user friends completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Get user friends failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure(message);
            }

            try
            {
                // Process all rows for successful results
                do
                {
                    // Skip rows that don't have friend data (error rows)
                    if (reader.GetValueOrDefault<int>("FriendId") is default(int))
                    {
                        var friendDto = FriendshipMapper.MapFriendsResponseFromReader(reader);
                        friends.Add(friendDto);
                    }
                } while (await reader.ReadAsync());

                logger.LogInformation("Successfully retrieved {FriendCount} friends for UserId {UserId}",
                    friends.Count, userId);

                return OperationResult<List<UserFriendsGetResponseDto>>.Success(friends,
                    $"Successfully retrieved {friends.Count} friends");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error mapping user friends result for UserId {UserId}", userId);
                return OperationResult<List<UserFriendsGetResponseDto>>.Failure("Failed to process user friends result");
            }
        }

    }
}