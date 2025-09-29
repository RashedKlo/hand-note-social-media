using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Friendship.Queries
{
    public class SearchUserFriendsQuery
    {
        private const string SearchUserFriendsSql = @"
            EXEC [dbo].[sp_SearchUserFriends] 
                @UserId, @Filter, @Page, @Limit";

        public static async Task<OperationResult<SearchUserFriendsResponseDto>> ExecuteAsync(
            SearchUserFriendsRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("SearchUserFriendsQuery received null data");
                return OperationResult<SearchUserFriendsResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing search user friends for UserId: {UserId}, Filter: {Filter}, Page: {Page}, Limit: {Limit}",
                dto.UserId, dto.Filter, dto.Page, dto.Limit);

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
                logger.LogError(ex, "Database connection failed during search user friends for UserId {UserId}",
                    dto.UserId);
                return OperationResult<SearchUserFriendsResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during search user friends for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<SearchUserFriendsResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during search user friends for UserId {UserId}",
                    dto.UserId);
                return OperationResult<SearchUserFriendsResponseDto>.Failure("Search user friends failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, SearchUserFriendsRequestDto dto)
        {
            var command = new SqlCommand(SearchUserFriendsSql, connection);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@Filter", dto.Filter);
            command.AddParameter("@Page", dto.Page);
            command.AddParameter("@Limit", dto.Limit);
            return command;
        }

        private static async Task<OperationResult<SearchUserFriendsResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            var response = new SearchUserFriendsResponseDto();
            var friends = new List<FriendDto>();

            while (await reader.ReadAsync())
            {
                var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
                
                if (status != "SUCCESS")
                {
                    var errorMessage = reader.GetValueOrDefault<string>("ErrorMessage") ?? "Search user friends failed";
                    var errorNumber = reader.GetValueOrDefault<int?>("ErrorNumber");
                    logger.LogWarning("Search user friends failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                        userId, errorMessage, errorNumber);
                    return OperationResult<SearchUserFriendsResponseDto>.Failure(errorMessage);
                }

                friends.Add(new FriendDto
                {
                    FriendId = reader.GetValueOrDefault<int>("FriendId"),
                    FriendUsername = reader.GetValueOrDefault<string>("FriendUsername") ?? string.Empty,
                    FriendFirstName = reader.GetValueOrDefault<string>("FriendFirstName") ?? string.Empty,
                    FriendLastName = reader.GetValueOrDefault<string>("FriendLastName") ?? string.Empty,
                    FriendProfilePicture = reader.GetValueOrDefault<string>("FriendProfilePicture"),
                    FriendEmail = reader.GetValueOrDefault<string>("FriendEmail") ?? string.Empty,
                    FriendsSince = reader.GetValueOrDefault<DateTime?>("FriendsSince"),
                    FriendshipId = reader.GetValueOrDefault<int>("FriendshipId")
                });

                response.TotalCount = reader.GetValueOrDefault<int>("TotalCount");
                response.CurrentPage = reader.GetValueOrDefault<int>("CurrentPage");
                response.PageSize = reader.GetValueOrDefault<int>("PageSize");
                response.TotalPages = reader.GetValueOrDefault<int>("TotalPages");
                response.Status = status;
            }

            response.Friends = friends;

            logger.LogInformation("User friends search completed successfully - UserId: {UserId}, Count: {Count}",
                userId, friends.Count);

            return OperationResult<SearchUserFriendsResponseDto>.Success(response, "User friends search completed successfully");
        }
    }
}
