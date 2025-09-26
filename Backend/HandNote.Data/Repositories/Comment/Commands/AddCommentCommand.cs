using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Comment.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Comment.Commands
{
    public class AddCommentCommand
    {
        private const string AddCommentSql = @"
            EXEC [dbo].[sp_AddComment] 
                @PostId, @UserId, @ParentCommentId, @Content, @MediaId";

        public static async Task<OperationResult<CommentCreateResponseDto>> ExecuteAsync(
            CommentCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("AddCommentCommand received null comment data");
                return OperationResult<CommentCreateResponseDto>.Failure("Comment data is required");
            }

            logger.LogInformation("Executing comment creation for PostId: {PostId}, UserId: {UserId}",
                dto.PostId, dto.UserId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.PostId, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53)
            {
                logger.LogError(ex, "Database connection failed during comment creation for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<CommentCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during comment creation for PostId {PostId}, UserId {UserId}. Error: {Error}",
                    dto.PostId, dto.UserId, ex.Message);
                return OperationResult<CommentCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during comment creation for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<CommentCreateResponseDto>.Failure("Comment creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommentCreateRequestDto dto)
        {
            var command = new SqlCommand(AddCommentSql, connection);
            command.AddParameter("@PostId", dto.PostId);
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@ParentCommentId", dto.ParentCommentId);
            command.AddParameter("@Content", dto.Content);
            command.AddParameter("@MediaId", dto.MediaId);
            return command;
        }

        private static async Task<OperationResult<CommentCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int postId,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from comment creation procedure for PostId {PostId}, UserId {UserId}",
                    postId, userId);
                return OperationResult<CommentCreateResponseDto>.Failure("Comment creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Comment creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Comment creation failed for PostId {PostId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    postId, userId, message, errorNumber);
                return OperationResult<CommentCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var responseDto = CommentMapper.MapCreateResponseFromReader(reader);

                    logger.LogInformation("Comment created successfully - CommentId: {CommentId}, PostId: {PostId}, UserId: {UserId}",
                        responseDto.CommentId, postId, userId);

                    return OperationResult<CommentCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping comment creation result for PostId {PostId}, UserId {UserId}",
                        postId, userId);
                    return OperationResult<CommentCreateResponseDto>.Failure("Failed to process comment creation result");
                }
            }
        }
    }
}