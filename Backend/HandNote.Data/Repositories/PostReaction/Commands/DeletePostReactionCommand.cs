
using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.PostReaction.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.PostReaction.Commands
{
    public class DeletePostReactionCommand
    {
        private const string DeletePostReactionSql = @"
            EXEC [dbo].[sp_RemovePostReaction] 
                @PostId, @UserId";

        public static async Task<OperationResult<PostReactionDeleteResponseDto>> ExecuteAsync(
            PostReactionDeleteRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("DeletePostReactionCommand received null reaction data");
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Reaction data is required");
            }

            logger.LogInformation("Executing post reaction deletion for PostId: {PostId}, UserId: {UserId}",
                dto.PostId, dto.UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during post reaction deletion for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during post reaction deletion for PostId {PostId}, UserId {UserId}. Error: {Error}",
                    dto.PostId, dto.UserId, ex.Message);
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during post reaction deletion for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Post reaction deletion failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, PostReactionDeleteRequestDto dto)
        {
            var command = new SqlCommand(DeletePostReactionSql, connection);

            // Add parameters to prevent SQL injection
            command.AddParameter("@PostId", dto.PostId);
            command.AddParameter("@UserId", dto.UserId);

            return command;
        }

        private static async Task<OperationResult<PostReactionDeleteResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            PostReactionDeleteRequestDto dto)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from post reaction deletion procedure for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Post reaction deletion procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Post reaction deletion completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Post reaction deletion failed for PostId {PostId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    dto.PostId, dto.UserId, message, errorNumber);
                return OperationResult<PostReactionDeleteResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    // Map the reaction deletion data from stored procedure result
                    var responseDto = PostReactionMapper.MapDeleteResponseFromReader(reader);

                    logger.LogInformation("Post reaction deleted successfully - ReactionId: {ReactionId}, PostId: {PostId}, UserId: {UserId}, RemovedReactionType: {RemovedReactionType}",
                        responseDto.ReactionId, dto.PostId, dto.UserId, responseDto.RemovedReactionType);

                    return OperationResult<PostReactionDeleteResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping post reaction deletion result for PostId {PostId}, UserId {UserId}",
                        dto.PostId, dto.UserId);
                    return OperationResult<PostReactionDeleteResponseDto>.Failure("Failed to process post reaction deletion result");
                }
            }
        }
    }
}