using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.PostReaction.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.PostReaction.Commands
{
    public class CreatePostReactionCommand
    {
        private const string CreatePostReactionSql = @"
            EXEC [dbo].[sp_AddPostReaction] 
                @PostId, @UserId, @ReactionType";

        public static async Task<OperationResult<PostReactionCreateResponseDto>> ExecuteAsync(
            PostReactionCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreatePostReactionCommand received null reaction data");
                return OperationResult<PostReactionCreateResponseDto>.Failure("Reaction data is required");
            }

            logger.LogInformation("Executing post reaction creation for PostId: {PostId}, UserId: {UserId}, ReactionType: {ReactionType}",
                dto.PostId, dto.UserId, dto.ReactionType);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.PostId, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during post reaction creation for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostReactionCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during post reaction creation for PostId {PostId}, UserId {UserId}. Error: {Error}",
                    dto.PostId, dto.UserId, ex.Message);
                return OperationResult<PostReactionCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during post reaction creation for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostReactionCreateResponseDto>.Failure("Post reaction creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, PostReactionCreateRequestDto dto)
        {
            var command = new SqlCommand(CreatePostReactionSql, connection);

            // Add parameters to prevent SQL injection
            command.AddParameter("@PostId", dto.PostId);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@ReactionType", dto.ReactionType);

            return command;
        }

        private static async Task<OperationResult<PostReactionCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int postId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from post reaction creation procedure for PostId {PostId}, UserId {UserId}",
                    postId, userId);
                return OperationResult<PostReactionCreateResponseDto>.Failure("Post reaction creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Post reaction creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Post reaction creation failed for PostId {PostId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    postId, userId, message, errorNumber);
                return OperationResult<PostReactionCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    // Map the reaction data from stored procedure result
                    var responseDto = PostReactionMapper.MapCreateResponseFromReader(reader);

                    logger.LogInformation("Post reaction changed successfully - ReactionId: {ReactionId}, PostId: {PostId}, UserId: {UserId}, ReactionType: {ReactionType}",
                       responseDto.ReactionId, postId, userId, responseDto.ReactionType);

                    return OperationResult<PostReactionCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping post reaction creation result for PostId {PostId}, UserId {UserId}",
                        postId, userId);
                    return OperationResult<PostReactionCreateResponseDto>.Failure("Failed to process post reaction creation result");
                }
            }
        }
    }
}