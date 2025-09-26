using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Comment.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Comment.Commands
{
    public class DeleteCommentCommand
    {
        private const string DeleteCommentSql = @"
            EXEC [dbo].[sp_DeleteComment] 
                @CommentId, @UserId";

        public static async Task<OperationResult<CommentDeleteResponseDto>> ExecuteAsync(
            CommentDeleteRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("DeleteCommentCommand received null comment data");
                return OperationResult<CommentDeleteResponseDto>.Failure("Comment data is required");
            }

            logger.LogInformation("Executing comment deletion for CommentId: {CommentId}, UserId: {UserId}",
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
                logger.LogError(ex, "Database connection failed during comment deletion for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentDeleteResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during comment deletion for CommentId {CommentId}, UserId {UserId}. Error: {Error}",
                    dto.CommentId, dto.UserId, ex.Message);
                return OperationResult<CommentDeleteResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during comment deletion for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentDeleteResponseDto>.Failure("Comment deletion failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommentDeleteRequestDto dto)
        {
            var command = new SqlCommand(DeleteCommentSql, connection);
            command.AddParameter("@CommentId", dto.CommentId);
            command.AddParameter("@UserId", dto.UserId);
            return command;
        }

        private static async Task<OperationResult<CommentDeleteResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int commentId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from comment deletion procedure for CommentId {CommentId}, UserId {UserId}",
                    commentId, userId);
                return OperationResult<CommentDeleteResponseDto>.Failure("Comment deletion procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Comment deletion completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Comment deletion failed for CommentId {CommentId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    commentId, userId, message, errorNumber);
                return OperationResult<CommentDeleteResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = CommentMapper.MapDeleteResponseFromReader(reader);

                    logger.LogInformation("Comment deleted successfully - CommentId: {CommentId}, UserId: {UserId}",
                        commentId, userId);

                    return OperationResult<CommentDeleteResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping comment deletion result for CommentId {CommentId}, UserId {UserId}",
                        commentId, userId);
                    return OperationResult<CommentDeleteResponseDto>.Failure("Failed to process comment deletion result");
                }
            }
        }
    }
}