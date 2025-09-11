using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Media_Files;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.Helpers;
using HandNote.Data.Repositories.Post.Helpers;
using HandNote.Data.Results;
using HandNote.Data.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Post.Commands
{
    public class CreatePostCommand
    {
        private const string CreatePostSql = @"
            EXEC [dbo].[sp_CreatePost] 
                @UserId, @Content, @PostType, @SharedPostId, @HasMedia, @AllowsComments, @AllowsShares";

        public static async Task<OperationResult<PostCreateResponseDto>> ExecuteAsync(
            PostCreateRequestDto dto,
            ILogger logger)
        {
            if (dto == null)
            {
                logger.LogError("CreatePostCommand received null post data");
                return OperationResult<PostCreateResponseDto>.Failure("Post data is required");
            }



            logger.LogInformation("Executing post creation for UserId: {UserId}, PostType: {PostType}", dto.UserId, dto.PostType);

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = CreateCommand(connection, dto);
                using var reader = await command.ExecuteReaderAsync();

                return await ProcessResultAsync(reader, logger, dto.UserId);
            }
            catch (SqlException ex) when (ex.Number is 2 or 53) // Connection timeout or network issues
            {
                logger.LogError(ex, "Database connection failed during post creation for UserId {UserId}", dto.UserId);
                return OperationResult<PostCreateResponseDto>.Failure("Database connection failed. Please try again.");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Database error during post creation for UserId {UserId}. Error: {Error}",
                    dto.UserId, ex.Message);
                return OperationResult<PostCreateResponseDto>.Failure("Database operation failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during post creation for UserId {UserId}", dto.UserId);
                return OperationResult<PostCreateResponseDto>.Failure("Post creation failed due to system error");
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, PostCreateRequestDto dto)
        {
            var command = new SqlCommand(CreatePostSql, connection);

            // Add parameters to prevent SQL injection
            command.AddParameter("@UserId", dto.UserId);
            command.AddParameter("@Content", dto.Content);
            command.AddParameter("@PostType", dto.PostType);
            command.AddParameter("@SharedPostId", dto.SharedPostId);
            command.AddParameter("@HasMedia", dto.HasMedia);
            command.AddParameter("@AllowsComments", dto.AllowsComments);
            command.AddParameter("@AllowsShares", dto.AllowsShares);

            return command;
        }

        private static async Task<OperationResult<PostCreateResponseDto>> ProcessResultAsync(
            SqlDataReader reader,
            ILogger logger,
            int userId)
        {
            if (!await reader.ReadAsync())
            {
                logger.LogWarning("No result returned from post creation procedure for UserId {UserId}", userId);
                return OperationResult<PostCreateResponseDto>.Failure("Post creation procedure returned no result");
            }

            var status = reader.GetValueOrDefault<string>("Status") ?? "Error";
            var message = reader.GetValueOrDefault<string>("Message") ?? "Post creation completed";

            if (status != "SUCCESS")
            {
                var errorNumber = reader.GetValueOrDefault<int>("ErrorNumber");
                logger.LogWarning("Post creation failed for UserId {UserId}: {Message} (Error: {ErrorNumber})",
                    userId, message, errorNumber);
                return OperationResult<PostCreateResponseDto>.Failure(message);
            }
            else
            {
                try
                {
                    // Map the post data
                    var post = PostMapper.MapPostFromReader(reader);

                    // Parse media files from JSON
                    var mediaFilesJson = reader.GetValueOrDefault<string>("MediaFiles") ?? "[]";
                    var mediaFiles = ParseMediaFiles(mediaFilesJson, logger);
                    var mediaCount = reader.GetValueOrDefault<int>("MediaCount");

                    var responseDto = new PostCreateResponseDto
                    {
                        Post = post,
                        MediaFiles = mediaFiles,
                        MediaCount = mediaCount
                    };

                    logger.LogInformation("Post created successfully - PostId: {PostId}, UserId: {UserId}, MediaCount: {MediaCount}",
                        post?.PostId, userId, mediaCount);

                    return OperationResult<PostCreateResponseDto>.Success(responseDto, message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error mapping post creation result for UserId {UserId}", userId);
                    return OperationResult<PostCreateResponseDto>.Failure("Failed to process post creation result");
                }
            }
        }

        private static List<MediaFileDto?> ParseMediaFiles(string mediaFilesJson, ILogger logger)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mediaFilesJson) || mediaFilesJson == "[]")
                {
                    return new List<MediaFileDto?>();
                }

                var mediaFiles = JsonSerializer.Deserialize<List<MediaFileDto>>(mediaFilesJson);
                return mediaFiles?.Cast<MediaFileDto?>().ToList() ?? new List<MediaFileDto?>();
            }
            catch (JsonException ex)
            {
                logger.LogWarning(ex, "Failed to parse media files JSON: {Json}", mediaFilesJson);
                return new List<MediaFileDto?>();
            }
        }
    }

}