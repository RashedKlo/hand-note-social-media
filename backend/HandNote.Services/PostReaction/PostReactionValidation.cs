using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.PostReaction;

public static class PostReactionValidation
{
    private static readonly string[] ValidReactionTypes = { "like", "love", "haha", "wow", "sad", "angry", "care" };

    public static async Task<OperationResult<bool>> ValidatePostReactionCreationAsync(
        PostReactionCreateRequestDto dto,
        IPostRepository postRepository, // Changed from IPostReactionRepository
        ILogger logger)
    {
        // Basic validation
        if (dto.PostId <= 0)
        {
            return OperationResult<bool>.Failure("PostId must be a positive integer");
        }

        if (dto.UserId <= 0)
        {
            return OperationResult<bool>.Failure("UserId must be a positive integer");
        }

        if (string.IsNullOrWhiteSpace(dto.ReactionType))
        {
            return OperationResult<bool>.Failure("ReactionType is required");
        }

        if (!ValidReactionTypes.Contains(dto.ReactionType.ToLower()))
        {
            return OperationResult<bool>.Failure("Invalid reaction type. Must be: like, love, haha, wow, sad, angry, or care");
        }

        // Check if post exists using IPostRepository
        var postExistenceResult = await postRepository.IsPostExistedById(dto.PostId);
        if (!postExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check post existence for PostId {PostId}: {Message}",
                dto.PostId, postExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify post existence");
        }

        if (postExistenceResult.Data?.Exists != true)
        {
            logger.LogWarning("Post reaction creation attempted for non-existent PostId {PostId}", dto.PostId);
            return OperationResult<bool>.Failure("Post not found or has been deleted");
        }

        logger.LogDebug("Post reaction creation validation passed for PostId: {PostId}, UserId: {UserId}",
            dto.PostId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

    public static async Task<OperationResult<bool>> ValidatePostReactionDeletionAsync(
        PostReactionDeleteRequestDto dto,
        IPostRepository postRepository, // Changed from IPostReactionRepository
        ILogger logger)
    {
        // Basic validation
        if (dto.PostId <= 0)
        {
            return OperationResult<bool>.Failure("PostId must be a positive integer");
        }

        if (dto.UserId <= 0)
        {
            return OperationResult<bool>.Failure("UserId must be a positive integer");
        }

        // Check if post exists using IPostRepository
        var postExistenceResult = await postRepository.IsPostExistedById(dto.PostId);
        if (!postExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check post existence for PostId {PostId}: {Message}",
                dto.PostId, postExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify post existence");
        }

        if (postExistenceResult.Data?.Exists != true)
        {
            logger.LogWarning("Post reaction deletion attempted for non-existent PostId {PostId}", dto.PostId);
            return OperationResult<bool>.Failure("Post not found or has been deleted");
        }

        logger.LogDebug("Post reaction deletion validation passed for PostId: {PostId}, UserId: {UserId}",
            dto.PostId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }
}