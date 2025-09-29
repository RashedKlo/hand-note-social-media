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
    public class GetFriendRequestsQuery
    {
        private const string GetFriendRequestsSql = @"
            EXEC [dbo].[sp_GetUserFriendRequests] 
                @UserId, @Page, @Limit";

        public static async Task<OperationResult<GetFriendRequestsResponseDto>> ExecuteAsync(
            GetFriendRequestsRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("GetFriendRequestsQuery received null data");
                return OperationResult<GetFriendRequestsResponseDto>.Failure("Request data is required");
            }

            logger.LogInformation("Executing get friend requests for UserId: {UserId}, Page: {Page}, Limit: {Limit}",
                dto.UserId, dto.Page, dto.Limit);

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
                logger.LogError(ex, "Database connection failed during get friend requests for UserId {UserId}",
                    dto.UserId);
                return OperationResult<GetFriendRequestsResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during get friend requests for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<GetFriendRequestsResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during get friend requests for UserId {UserId}",
                    dto.UserId);
                return OperationResult<GetFriendRequestsResponseDto>.Failure("Get friend requests failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, GetFriendRequestsRequestDto dto)
        {
            var command = new SqlCommand(GetFriendRequestsSql, connection);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@Page", dto.Page);
            command.AddParameter("@Limit", dto.Limit);
            return command;
        }

        private static async Task<OperationResult<GetFriendRequestsResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            var response = new GetFriendRequestsResponseDto();
            var friendRequests = new List<FriendRequestDto>();

            while (await reader.ReadAsync())
            {
                var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
                
                if (status != "SUCCESS")
                {
                    var errorMessage = reader.GetValueOrDefault<string>("ErrorMessage") ?? "Get friend requests failed";
                    var errorNumber = reader.GetValueOrDefault<int?>("ErrorNumber");
                    logger.LogWarning("Get friend requests failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                        userId, errorMessage, errorNumber);
                    return OperationResult<GetFriendRequestsResponseDto>.Failure(errorMessage);
                }

                friendRequests.Add(new FriendRequestDto
                {
                    RequesterId = reader.GetValueOrDefault<int>("RequesterId"),
                    RequesterUsername = reader.GetValueOrDefault<string>("RequesterUsername") ?? string.Empty,
                    RequesterFirstName = reader.GetValueOrDefault<string>("RequesterFirstName") ?? string.Empty,
                    RequesterLastName = reader.GetValueOrDefault<string>("RequesterLastName") ?? string.Empty,
                    RequesterProfilePicture = reader.GetValueOrDefault<string>("RequesterProfilePicture"),
                    RequesterEmail = reader.GetValueOrDefault<string>("RequesterEmail") ?? string.Empty,
                    RequestSentAt = reader.GetValueOrDefault<DateTime?>("RequestSentAt"),
                    FriendshipId = reader.GetValueOrDefault<int>("FriendshipId")
                });

                response.TotalCount = reader.GetValueOrDefault<int>("TotalCount");
                response.CurrentPage = reader.GetValueOrDefault<int>("CurrentPage");
                response.PageSize = reader.GetValueOrDefault<int>("PageSize");
                response.TotalPages = reader.GetValueOrDefault<int>("TotalPages");
                response.Status = status;
            }

            response.FriendRequests = friendRequests;

            logger.LogInformation("Friend requests retrieved successfully - UserId: {UserId}, Count: {Count}",
                userId, friendRequests.Count);

            return OperationResult<GetFriendRequestsResponseDto>.Success(response, "Friend requests retrieved successfully");
        }
    }
}