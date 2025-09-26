// UpdateCommentCommand.cs
using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Comment.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Comment.Commands
{
    public class UpdateCommentCommand
    {
        private const string UpdateCommentSql = @"
            EXEC [dbo].[sp_UpdateComment] 
                @CommentId, @UserId, @Content, @MediaId";

        public static async Task<OperationResult<CommentUpdateResponseDto>> ExecuteAsync(
            CommentUpdateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("UpdateCommentCommand received null comment data");
                return OperationResult<CommentUpdateResponseDto>.Failure("Comment data is required");
            }

            logger.LogInformation("Executing comment update for CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

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
                logger.LogError(ex, "Database connection failed during comment update for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentUpdateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during comment update for CommentId {CommentId}, UserId {UserId}. Error: {Error}",
                    dto.CommentId, dto.UserId, ex.Message);
                return OperationResult<CommentUpdateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during comment update for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentUpdateResponseDto>.Failure("Comment update failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommentUpdateRequestDto dto)
        {
            var command = new SqlCommand(UpdateCommentSql, connection);
            command.AddParameter("@CommentId", dto.CommentId);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@Content", dto.Content);
            command.AddParameter("@MediaId", dto.MediaId);
            return command;
        }

        private static async Task<OperationResult<CommentUpdateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int commentId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from comment update procedure for CommentId {CommentId}, UserId {UserId}",
                    commentId, userId);
                return OperationResult<CommentUpdateResponseDto>.Failure("Comment update procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Comment update completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Comment update failed for CommentId {CommentId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    commentId, userId, message, errorNumber);
                return OperationResult<CommentUpdateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = CommentMapper.MapUpdateResponseFromReader(reader);

                    logger.LogInformation("Comment updated successfully - CommentId: {CommentId}, UserId: {UserId}",
                        commentId, userId);

                    return OperationResult<CommentUpdateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping comment update result for CommentId {CommentId}, UserId {UserId}",
                        commentId, userId);
                    return OperationResult<CommentUpdateResponseDto>.Failure("Failed to process comment update result");
                }
            }
        }
    }
}