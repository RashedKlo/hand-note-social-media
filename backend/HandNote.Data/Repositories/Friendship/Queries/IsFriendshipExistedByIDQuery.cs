using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Friendship.Helpers;

using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Friendship.Queries
{
    public class IsFriendshipExistedByIDQuery
    {
        private const string IsFriendshipExistedSql = @"
            EXEC [dbo].[sp_IsFriendshipExisted] 
                @UserId1, @UserId2";

        public static async Task<OperationResult<FriendshipExistenceResponseDto>> ExecuteAsync(
           FriendshipExistenceRequestDto dto,
            ILogger logger)
        {
            if (dto.UserId1 <= 0)
            {
                logger.LogError("IsFriendshipExistedByIDQuery received invalid UserId1: {UserId1}", dto.UserId1);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("UserId1 is required and must be a positive integer");
            }

            if (dto.UserId2 <= 0)
            {
                logger.LogError("IsFriendshipExistedByIDQuery received invalid UserId2: {UserId2}", dto.UserId2);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("UserId2 is required and must be a positive integer");
            }

            logger.LogInformation("Executing friendship existence check for UserId1: {UserId1}, UserId2: {UserId2}", dto.UserId1, dto.UserId2);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto.UserId1, dto.UserId2);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserId1, dto.UserId2);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during friendship existence check for UserId1 {UserId1}, UserId2 {UserId2}",
                    dto.UserId1, dto.UserId2);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during friendship existence check for UserId1 {UserId1}, UserId2 {UserId2}. Error: {Error}",
                    dto.UserId1, dto.UserId2, ex.Message);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during friendship existence check for UserId1 {UserId1}, UserId2 {UserId2}",
                    dto.UserId1, dto.UserId2);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("Friendship existence check failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int userId1, int userId2)
        {
            var command = new SqlCommand(IsFriendshipExistedSql, connection);
            command.AddParameter("@UserId1", userId1);
            command.AddParameter("@UserId2", userId2);
            return command;
        }

        private static async Task<OperationResult<FriendshipExistenceResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId1,
            int userId2)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from friendship existence check procedure for UserId1 {UserId1}, UserId2 {UserId2}",
                    userId1, userId2);
                return OperationResult<FriendshipExistenceResponseDto>.Failure("Friendship existence check procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Friendship existence check completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Friendship existence check failed for UserId1 {UserId1}, UserId2 {UserId2}: {Message} (Error: {ErrorNumber})",
                    userId1, userId2, message, errorNumber);
                return OperationResult<FriendshipExistenceResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = FriendshipMapper.MapExistenceResponseFromReader(reader);

                    logger.LogInformation("Friendship existence check completed successfully - UserId1: {UserId1}, UserId2: {UserId2}, Exists: {FriendshipExists}, Status: {FriendshipStatus}",
                        userId1, userId2, responseDto.FriendshipExists, responseDto.FriendshipStatus);

                    return OperationResult<FriendshipExistenceResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping friendship existence check result for UserId1 {UserId1}, UserId2 {UserId2}",
                        userId1, userId2);
                    return OperationResult<FriendshipExistenceResponseDto>.Failure("Failed to process friendship existence check result");
                }
            }
        }
    }
}