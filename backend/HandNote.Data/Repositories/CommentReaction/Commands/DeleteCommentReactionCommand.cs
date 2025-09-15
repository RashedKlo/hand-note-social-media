using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.CommentReaction.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.CommentReaction.Commands;

public class DeleteCommentReactionCommand
{
    private const string DeleteCommentReactionSql = @"
            EXEC [dbo].[sp_RemoveCommentReaction] 
                @CommentId, @UserId";

    public static async Task<OperationResult<CommentReactionDeleteResponseDto>> ExecuteAsync(
        CommentReactionDeleteRequestDto dto,
        ILogger logger)
    {
        if (dto == null)
        {
            logger.LogError("DeleteCommentReactionCommand received null reaction data");
            return OperationResult<CommentReactionDeleteResponseDto>.Failure("Reaction data is required");
        }

        logger.LogInformation("Executing comment reaction deletion for CommentId: {CommentId}, UserId: {UserId}",
            dto.CommentId, dto.UserId);

        try
        {
            using var connection = new SqlConnection(DBSettings.connectionString);
            await connection.OpenAsync();

            using var command = CreateCommand(connection, dto);
            using var reader = await command.ExecuteReaderAsync();

            return await ProcessResultAsync(reader, logger, dto);
        }
        catch (SqlException ex) when (ex.Number is 2 or 53)
        {
            logger.LogError(ex, "Database connection failed during comment reaction deletion for CommentId {CommentId}, UserId {UserId}",
                dto.CommentId, dto.UserId);
            return OperationResult<CommentReactionDeleteResponseDto>.Failure("Database connection failed. Please try again.");
        }
        catch (SqlException ex)
        {
            logger.LogError(ex, "Database error during comment reaction deletion for CommentId {CommentId}, UserId {UserId}. Error: {Error}",
                dto.CommentId, dto.UserId, ex.Message);
            return OperationResult<CommentReactionDeleteResponseDto>.Failure("Database operation failed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during comment reaction deletion for CommentId {CommentId}, UserId {UserId}",
                dto.CommentId, dto.UserId);
            return OperationResult<CommentReactionDeleteResponseDto>.Failure("Comment reaction deletion failed due to system error");
        }
    }

    private static SqlCommand CreateCommand(SqlConnection connection, CommentReactionDeleteRequestDto dto)
    {
        var command = new SqlCommand(DeleteCommentReactionSql, connection);
        command.AddParameter("@CommentId", dto.CommentId);
        command.AddParameter("@UserId", dto.UserId);
        return command;
    }

    private static async Task<OperationResult<CommentReactionDeleteResponseDto>> ProcessResultAsync(
        SqlDataReader reader,
        ILogger logger,
        CommentReactionDeleteRequestDto dto)
    {
        if (!await reader.ReadAsync())
        {
            logger.LogWarning("No result returned from comment reaction deletion procedure for CommentId {CommentId}, UserId {UserId}",
                dto.CommentId, dto.UserId);
            return OperationResult<CommentReactionDeleteResponseDto>.Failure("Comment reaction deletion procedure returned no result");
        }

        var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
        var message = reader.GetValueOrDefault<string>("Message") ?? "Comment reaction deletion completed";

        if (status != "SUCCESS")
        {
            var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
            logger.LogWarning("Comment reaction deletion failed for CommentId {CommentId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                dto.CommentId, dto.UserId, message, errorNumber);
            return OperationResult<CommentReactionDeleteResponseDto>.Failure(message);
        }
        else
        {
            try
            {
                var responseDto = CommentReactionMapper.MapDeleteResponseFromReader(reader);

                logger.LogInformation("Comment reaction deleted successfully - ReactionId: {ReactionId}, CommentId: {CommentId}, UserId: {UserId}, RemovedReactionType: {RemovedReactionType}",
                    responseDto.ReactionId, dto.CommentId, dto.UserId, responseDto.RemovedReactionType);

                return OperationResult<CommentReactionDeleteResponseDto>.Success(responseDto, message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error mapping comment reaction deletion result for CommentId {CommentId}, UserId {UserId}",
                    dto.CommentId, dto.UserId);
                return OperationResult<CommentReactionDeleteResponseDto>.Failure("Failed to process comment reaction deletion result");
            }
        }
    }
}
