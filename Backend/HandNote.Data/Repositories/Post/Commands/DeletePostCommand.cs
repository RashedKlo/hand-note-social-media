using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Post.Commands
{
    public class DeletePostCommand
    {
        private const string DeletePostSql = @"
            EXEC [dbo].[sp_DeletePost] 
                @PostId, @UserId";

        public static async Task<OperationResult<PostDeleteResponseDto>> ExecuteAsync(
            PostDeleteRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("DeletePostCommand received null post data");
                return OperationResult<PostDeleteResponseDto>.Failure("Post data is required");
            }

            logger.LogInformation("Executing post deletion for PostId: {PostId}, UserId: {UserId}",
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
                logger.LogError(ex, "Database connection failed during post deletion for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostDeleteResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during post deletion for PostId {PostId}, UserId {UserId}. Error: {Error}",
                    dto.PostId, dto.UserId, ex.Message);
                return OperationResult<PostDeleteResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during post deletion for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostDeleteResponseDto>.Failure("Post deletion failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, PostDeleteRequestDto dto)
        {
            var command = new SqlCommand(DeletePostSql, connection);

            // Add parameters to prevent SQL injection
            command.AddParameter("@PostId", dto.PostId);
            command.AddParameter("@UserId", dto.UserId);

            return command;
        }

        private static async Task<OperationResult<PostDeleteResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
        PostDeleteRequestDto dto)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from post deletion procedure for PostId {PostId}, UserId {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<PostDeleteResponseDto>.Failure("Post deletion procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Post deletion completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Post deletion failed for PostId {PostId}, UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    dto.PostId, dto.UserId, message, errorNumber);
                return OperationResult<PostDeleteResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var deletedRows = reader.GetValueOrDefault<int>("DeletedRows");
                    var deletedAt = reader.GetValueOrDefault<DateTime?>("DeletedAt");

                    var responseDto = new PostDeleteResponseDto
                    {
                        PostId = dto.PostId,
                        UserId = dto.UserId,
                        DeletedRows = deletedRows,
                        DeletedAt = deletedAt
                    };

                    logger.LogInformation("Post deleted successfully - PostId: {PostId}, UserId: {UserId}, DeletedRows: {DeletedRows}",
                        dto.PostId, dto.UserId, deletedRows);

                    return OperationResult<PostDeleteResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping post deletion result for PostId {PostId}, UserId {UserId}",
                        dto.PostId, dto.UserId);
                    return OperationResult<PostDeleteResponseDto>.Failure("Failed to process post deletion result");
                }
            }
        }
    }
}