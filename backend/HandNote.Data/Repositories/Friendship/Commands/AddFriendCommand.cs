using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Friendship;
using HandNote.Data.Repositories.Friendship.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Friendship.Commands
{
    public class AddFriendCommand
    {
        private const string AddFriendSql = @"
            EXEC [dbo].[sp_AddFriend] 
                @RequesterId, @AddresseeId";

        public static async Task<OperationResult<FriendshipCreateResponseDto>> ExecuteAsync(
            FriendshipCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("AddFriendCommand received null friendship data");
                return OperationResult<FriendshipCreateResponseDto>.Failure("Friendship data is required");
            }

            logger.LogInformation("Executing friend request creation for RequesterId: {RequesterId}, AddresseeId: {AddresseeId}",
                dto.RequesterId, dto.AddresseeId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.RequesterId, dto.AddresseeId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during friend request creation for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    dto.RequesterId, dto.AddresseeId);
                return OperationResult<FriendshipCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during friend request creation for RequesterId {RequesterId}, AddresseeId {AddresseeId}. Error: {Error}",
                    dto.RequesterId, dto.AddresseeId, ex.Message);
                return OperationResult<FriendshipCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during friend request creation for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    dto.RequesterId, dto.AddresseeId);
                return OperationResult<FriendshipCreateResponseDto>.Failure("Friend request creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, FriendshipCreateRequestDto dto)
        {
            var command = new SqlCommand(AddFriendSql, connection);
            command.AddParameter("@RequesterId", dto.RequesterId);
            command.AddParameter("@AddresseeId", dto.AddresseeId);
            return command;
        }

        private static async Task<OperationResult<FriendshipCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int requesterId,
            int addresseeId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from friend request creation procedure for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                    requesterId, addresseeId);
                return OperationResult<FriendshipCreateResponseDto>.Failure("Friend request creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Friend request creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Friend request creation failed for RequesterId {RequesterId}, AddresseeId {AddresseeId}: {Message} (Error: {ErrorNumber})",
                    requesterId, addresseeId, message, errorNumber);
                return OperationResult<FriendshipCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = FriendshipMapper.MapCreateResponseFromReader(reader);

                    logger.LogInformation("Friend request created successfully - FriendshipId: {FriendshipId}, RequesterId: {RequesterId}, AddresseeId: {AddresseeId}",
                        responseDto.FriendshipId, requesterId, addresseeId);

                    return OperationResult<FriendshipCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping friend request creation result for RequesterId {RequesterId}, AddresseeId {AddresseeId}",
                        requesterId, addresseeId);
                    return OperationResult<FriendshipCreateResponseDto>.Failure("Failed to process friend request creation result");
                }
            }
        }
    }
}