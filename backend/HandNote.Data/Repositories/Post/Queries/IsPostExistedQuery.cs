using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Queries;
using HandNote.Data.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Post.Queries
{
    public class IsPostExistedQuery
    {
        private const string IsPostExistedSql = @"
            EXEC [dbo].[sp_IsPostExisted] 
                @PostId";

        public static async Task<OperationResult<PostExistenceResponseDto>> ExecuteAsync(
            int PostId,
            ILogger logger)
        {
            if (PostId <= 0)
            {
                logger.LogError("IsPostExistedQuery received null post data");
                return OperationResult<PostExistenceResponseDto>.Failure("Post data is required");
            }

            logger.LogInformation("Checking post existence for PostId: {PostId}", PostId);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, PostId);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, PostId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during post existence check for PostId {PostId}",
                    PostId);
                return OperationResult<PostExistenceResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during post existence check for PostId {PostId}. Error: {Error}",
                    PostId, ex.Message);
                return OperationResult<PostExistenceResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during post existence check for PostId {PostId}",
                    PostId);
                return OperationResult<PostExistenceResponseDto>.Failure("Post existence check failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, int PostId)
        {
            var command = new SqlCommand(IsPostExistedSql, connection);

            // Add parameters to prevent SQL injection
            command.AddParameter("@PostId", PostId);

            return command;
        }

        private static async Task<OperationResult<PostExistenceResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int postId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from post existence check procedure for PostId {PostId}", postId);
                return OperationResult<PostExistenceResponseDto>.Failure("Post existence check procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Post existence check completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Post existence check failed for PostId {PostId}: {Message} (Error: {ErrorNumber})",
                    postId, message, errorNumber);
                return OperationResult<PostExistenceResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    var ownerPostId = reader.GetValueOrDefault<int>("OwnerPostID");

                    var responseDto = new PostExistenceResponseDto
                    {
                        PostId = postId,
                        Exists = true,
                        OwnerUserId = ownerPostId
                    };

                    logger.LogInformation("Post existence check successful - PostId: {PostId}, Exists: {Exists}, OwnerUserId: {OwnerUserId}",
                        postId, responseDto.Exists, ownerPostId);

                    return OperationResult<PostExistenceResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping post existence check result for PostId {PostId}", postId);
                    return OperationResult<PostExistenceResponseDto>.Failure("Failed to process post existence check result");
                }
            }
        }
    }
}