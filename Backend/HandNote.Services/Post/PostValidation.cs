using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.Post
{
    public static class PostValidation
    {

        // In your PostValidation class
        public static async Task<OperationResult<bool>> ValidatePostDeletionAsync(
            PostDeleteRequestDto dto,
            IPostRepository postRepository,
            ILogger logger)
        {
            try
            {
                var sharedPostExists = await postRepository.IsPostExistedById(dto.PostId);
                if (!sharedPostExists.IsSuccess || !sharedPostExists.Data!.Exists)
                {
                    logger.LogWarning(" post not found: {PostId} for UserId: {UserId}",
                        dto.PostId, dto.UserId);
                    return OperationResult<bool>.Failure(" post not found or has been deleted");
                }

                logger.LogDebug("Post deletion validation passed for PostId: {PostId}, UserId: {UserId}",
                    dto.PostId, dto.UserId);

                return OperationResult<bool>.Success(true, "Validation successful");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during post deletion validation for PostId: {PostId}, UserId: {UserId}",
                    dto.PostId, dto.UserId);
                return OperationResult<bool>.Failure("Validation failed due to system error");
            }
        }
        /// <summary>
        /// Validates post creation data based on stored procedure validation logic
        /// </summary>
        public static async Task<OperationResult<bool>> ValidatePostCreationAsync(
            PostCreateRequestDto dto,
            IPostRepository postRepository,
            ILogger logger)
        {

            logger.LogDebug("Starting post validation for UserId: {UserId}, PostType: {PostType}",
                dto.UserId, dto.PostType);

            try
            {

                // Validate post type specific rules
                var postTypeValidationResult = await ValidatePostTypeRulesAsync(dto, postRepository, logger);
                if (!postTypeValidationResult.IsSuccess)
                {
                    return postTypeValidationResult;
                }
                logger.LogDebug("Post validation completed successfully for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Success(true, "Validation passed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during post validation for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("An error occurred during validation");
            }
        }



        /// <summary>
        /// Validates post type specific rules (text, media, shared)
        /// </summary>
        private static async Task<OperationResult<bool>> ValidatePostTypeRulesAsync(
            PostCreateRequestDto dto,
            IPostRepository postRepository,
            ILogger logger)
        {
            var postType = dto.PostType.ToLowerInvariant();

            switch (postType)
            {
                case "text":
                    return ValidateTextPostRules(dto, logger);

                case "media":
                    return ValidateMediaPostRules(dto, logger);

                case "shared":
                    return await ValidateSharedPostRulesAsync(dto, postRepository, logger);

                default:
                    // This should not happen due to earlier validation
                    logger.LogError("Unexpected post type reached validation: {PostType}", dto.PostType);
                    return OperationResult<bool>.Failure("Invalid post type");
            }
        }

        /// <summary>
        /// Validates text post specific rules
        /// </summary>
        private static OperationResult<bool> ValidateTextPostRules(PostCreateRequestDto dto, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(dto.Content) && dto.HasMedia != true && dto.MediaFilesID == null)
            {
                logger.LogWarning("Text post has no content and no media for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("Text posts must have content or media");
            }

            // For text posts, SharedPostId must be null
            if (dto.SharedPostId != null)
            {
                logger.LogWarning("Text post has SharedPostId for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("SharedPostId must be null for text posts");
            }

            return OperationResult<bool>.Success(true);
        }

        /// <summary>
        /// Validates media post specific rules
        /// </summary>
        private static OperationResult<bool> ValidateMediaPostRules(PostCreateRequestDto dto, ILogger logger)
        {
            if (dto.HasMedia != true || dto.MediaFilesID == null)
            {
                logger.LogWarning("Media post has no media for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("Media posts must have media (HasMedia = true)");
            }

            // For media posts, SharedPostId must be null
            if (dto.SharedPostId != null)
            {
                logger.LogWarning("Media post has SharedPostId for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("SharedPostId must be null for media posts");
            }

            return OperationResult<bool>.Success(true);
        }

        /// <summary>
        /// Validates shared post specific rules
        /// </summary>
        private static async Task<OperationResult<bool>> ValidateSharedPostRulesAsync(
            PostCreateRequestDto dto,
            IPostRepository postRepository,
            ILogger logger)
        {
            if (dto.SharedPostId == null)
            {
                logger.LogWarning("Shared post missing SharedPostId for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("SharedPostId is required for shared post type");
            }
            if (dto.MediaFilesID != null)
            {
                logger.LogWarning("Shared post has media for UserId: {UserId}", dto.UserId);
                return OperationResult<bool>.Failure("Shared Post must be do not have media posts");
            }
            logger.LogDebug("Validating shared post: {SharedPostId} for UserId: {UserId}",
                dto.SharedPostId, dto.UserId);

            // Check if shared post exists
            var sharedPostExists = await postRepository.IsPostExistedById(dto.SharedPostId.Value);
            if (!sharedPostExists.IsSuccess || !sharedPostExists.Data!.Exists)
            {
                logger.LogWarning("Shared post not found: {SharedPostId} for UserId: {UserId}",
                    dto.SharedPostId, dto.UserId);
                return OperationResult<bool>.Failure("Shared post not found or has been deleted");
            }

            // Prevent self-sharing
            if (sharedPostExists.IsSuccess && sharedPostExists.Data.OwnerUserId == dto.UserId)
            {
                logger.LogWarning("User attempting to share own post: UserId: {UserId}, PostId: {PostId}",
                    dto.UserId, dto.SharedPostId);
                return OperationResult<bool>.Failure("Cannot share your own post");
            }

            return OperationResult<bool>.Success(true);
        }


    }
}