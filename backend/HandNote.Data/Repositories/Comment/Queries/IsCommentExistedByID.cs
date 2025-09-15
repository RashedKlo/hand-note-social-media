using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment;
using HandNote.Data.DTOs.Comment.Queries;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Comment.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Comment.Queries
{
    public class IsCommentExistedByIDQuery
    {
        private const string IsCommentExistedSql = @"
            EXEC [dbo].[sp_IsCommentExisted] 
                @CommentId";

        public static async Task<OperationResult<CommentExistenceResponseDto>> ExecuteAsync(
            int commentId,
            ILogger logger)
        {
            if (commentId <= 0)
            {
                logger.LogError("IsCommentExistedByIDQuery received invalid comment ID: {CommentId}", commentId);
                return OperationResult<CommentExistenceResponseDto>.Failure("Comment ID is required and must be a positive integer");
            }

            logger.LogInformation("Executing comment existence check for CommentId: {CommentId}", commentId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, commentId);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, commentId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during comment existence check for CommentId {CommentId}", commentId);
                return OperationResult<CommentExistenceResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during comment existence check for CommentId {CommentId}. Error: {Error}",
                    commentId, ex.Message);
                return OperationResult<CommentExistenceResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during comment existence check for CommentId {CommentId}", commentId);
                return OperationResult<CommentExistenceResponseDto>.Failure("Comment existence check failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int commentId)
        {
            var command = new SqlCommand(IsCommentExistedSql, connection);
            command.AddParameter("@CommentId", commentId);
            return command;
        }

        private static async Task<OperationResult<CommentExistenceResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int commentId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from comment existence check procedure for CommentId {CommentId}", commentId);
                return OperationResult<CommentExistenceResponseDto>.Failure("Comment existence check procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Comment existence check completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Comment existence check failed for CommentId {CommentId}: {Message} (Error: {ErrorNumber})",
                    commentId, message, errorNumber);
                return OperationResult<CommentExistenceResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = CommentMapper.MapExistenceResponseFromReader(reader);

                    logger.LogInformation("Comment existence check completed successfully - CommentId: {CommentId}, Exists: {CommentExists}, IsDeleted: {IsDeleted}",
                        commentId, responseDto.CommentExists, responseDto.IsDeleted);

                    return OperationResult<CommentExistenceResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping comment existence check result for CommentId {CommentId}", commentId);
                    return OperationResult<CommentExistenceResponseDto>.Failure("Failed to process comment existence check result");
                }
            }
        }
    }
}