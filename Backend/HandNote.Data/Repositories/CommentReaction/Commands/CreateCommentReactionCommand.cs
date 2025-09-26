using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.CommentReaction.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.CommentReaction.Commands
{
    public class CreateCommentReactionCommand
    {
        private const string CreateCommentReactionSql = @"
            EXEC [dbo].[sp_AddCommentReaction] 
                @CommentId, @UserId, @ReactionType";

        public static async Task<OperationResult<CommentReactionCreateResponseDto>> ExecuteAsync(
            CommentReactionCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreateCommentReactionCommand received null reaction data");
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Reaction data is required");
            }

            logger.LogInformation("Executing comment reaction creation for CommentId: {CommentId}, UserId: {UserId}, ReactionType: {ReactionType}",
                dto.CommentId, dto.UserId, dto.ReactionType);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.CommentId, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during comment reaction creation for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during comment reaction creation for CommentId {CommentId}, UserId {UserId}. Error: {Error}",
                    dto.CommentId, dto.UserId, ex.Message);
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during comment reaction creation for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Comment reaction creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommentReactionCreateRequestDto dto)
        {
            var command = new SqlCommand(CreateCommentReactionSql, connection);
            command.AddParameter("@CommentId", dto.CommentId);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@ReactionType", dto.ReactionType);
            return command;
        }

        private static async Task<OperationResult<CommentReactionCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int commentId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from comment reaction creation procedure for CommentId {CommentId}, UserId {UserId}",
                    commentId, userId);
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Comment reaction creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Comment reaction creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Comment reaction creation failed for CommentId {CommentId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    commentId, userId, message, errorNumber);
                return OperationResult<CommentReactionCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = CommentReactionMapper.MapCreateResponseFromReader(reader);
                    var actionType = responseDto.IsUpdate == true ? "updated" : "created";

                    logger.LogInformation("Comment reaction {ActionType} successfully - ReactionId: {ReactionId}, CommentId: {CommentId}, UserId: {UserId}, ReactionType: {ReactionType}",
                        actionType, responseDto.ReactionId, commentId, userId, responseDto.ReactionType);

                    return OperationResult<CommentReactionCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping comment reaction creation result for CommentId {CommentId}, UserId {UserId}",
                        commentId, userId);
                    return OperationResult<CommentReactionCreateResponseDto>.Failure("Failed to process comment reaction creation result");
                }
            }
        }
    }
}