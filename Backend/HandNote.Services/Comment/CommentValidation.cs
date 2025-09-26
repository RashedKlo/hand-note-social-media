// CommentValidation.cs
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.Comment.Queries;

public static class CommentValidation
{
    public static async Task<OperationResult<bool>> ValidateCommentCreationAsync(
        CommentCreateRequestDto dto,
        IPostRepository postRepository,
        ICommentRepository commentRepository,
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

        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            return OperationResult<bool>.Failure("Content is required and cannot be empty");
        }

        if (dto.Content.Trim().Length > 200)
        {
            return OperationResult<bool>.Failure("Content cannot exceed 200 characters");
        }

        if (dto.ParentCommentId.HasValue && dto.ParentCommentId.Value <= 0)
        {
            return OperationResult<bool>.Failure("ParentCommentId must be a positive integer when provided");
        }

        if (dto.MediaId.HasValue && dto.MediaId.Value <= 0)
        {
            return OperationResult<bool>.Failure("MediaId must be a positive integer when provided");
        }

        var postExistenceResult = await postRepository.IsPostExistedById(dto.PostId);
        if (!postExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check post existence for PostId {PostId}: {Message}",
                dto.PostId, postExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify post existence");
        }

        if (postExistenceResult.Data?.Exists != true)
        {
            logger.LogWarning("Comment creation attempted for non-existent PostId {PostId}", dto.PostId);
            return OperationResult<bool>.Failure("Post not found");
        }

        // Check parent comment if provided
        if (dto.ParentCommentId.HasValue)
        {
            var parentCommentExistenceResult = await commentRepository.IsCommentExistedByCommentID(dto.ParentCommentId.Value);
            if (!parentCommentExistenceResult.IsSuccess)
            {
                logger.LogWarning("Failed to check parent comment existence for CommentId {CommentId}: {Message}",
                    dto.ParentCommentId.Value, parentCommentExistenceResult.Message);
                return OperationResult<bool>.Failure("Unable to verify parent comment existence");
            }

            if (parentCommentExistenceResult.Data?.CommentExists != true || parentCommentExistenceResult.Data?.IsDeleted == true)
            {
                logger.LogWarning("Comment creation attempted with non-existent or deleted parent CommentId {CommentId}", dto.ParentCommentId.Value);
                return OperationResult<bool>.Failure("Parent comment not found or has been deleted");
            }
        }

        logger.LogDebug("Comment creation validation passed for PostId: {PostId}, UserId: {UserId}",
            dto.PostId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

    public static async Task<OperationResult<bool>> ValidateCommentUpdateAsync(
        CommentUpdateRequestDto dto,
        ICommentRepository commentRepository,
        ILogger logger)
    {
        // Basic validation
        if (dto.CommentId <= 0)
        {
            return OperationResult<bool>.Failure("CommentId must be a positive integer");
        }

        if (dto.UserId <= 0)
        {
            return OperationResult<bool>.Failure("UserId must be a positive integer");
        }

        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            return OperationResult<bool>.Failure("Content is required and cannot be empty");
        }

        if (dto.Content.Trim().Length > 200)
        {
            return OperationResult<bool>.Failure("Content cannot exceed 200 characters");
        }

        if (dto.MediaId.HasValue && dto.MediaId.Value <= 0)
        {
            return OperationResult<bool>.Failure("MediaId must be a positive integer when provided");
        }


        // Check if comment exists and is not deleted
        var commentExistenceResult = await commentRepository.IsCommentExistedByCommentID(dto.CommentId);
        if (!commentExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check comment existence for CommentId {CommentId}: {Message}",
                dto.CommentId, commentExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify comment existence");
        }

        if (commentExistenceResult.Data?.CommentExists != true)
        {
            logger.LogWarning("Comment update attempted for non-existent CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Comment not found");
        }

        if (commentExistenceResult.Data?.IsDeleted == true)
        {
            logger.LogWarning("Comment update attempted for deleted CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Cannot update a deleted comment");
        }
        logger.LogDebug("Comment update validation passed for CommentId: {CommentId}, UserId: {UserId}",
            dto.CommentId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

    public static async Task<OperationResult<bool>> ValidateCommentDeletionAsync(
        CommentDeleteRequestDto dto,
        ICommentRepository commentRepository,
        ILogger logger)
    {
        // Basic validation
        if (dto.CommentId <= 0)
        {
            return OperationResult<bool>.Failure("CommentId must be a positive integer");
        }

        if (dto.UserId <= 0)
        {
            return OperationResult<bool>.Failure("UserId must be a positive integer");
        }

        // Check if comment exists and is not already deleted
        var commentExistenceResult = await commentRepository.IsCommentExistedByCommentID(dto.CommentId);
        if (!commentExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check comment existence for CommentId {CommentId}: {Message}",
                dto.CommentId, commentExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify comment existence");
        }

        if (commentExistenceResult.Data?.CommentExists != true)
        {
            logger.LogWarning("Comment deletion attempted for non-existent CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Comment not found");
        }

        if (commentExistenceResult.Data?.IsDeleted == true)
        {
            logger.LogWarning("Comment deletion attempted for already deleted CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Comment is already deleted");
        }

        logger.LogDebug("Comment deletion validation passed for CommentId: {CommentId}, UserId: {UserId}",
            dto.CommentId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }
}
