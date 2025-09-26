// CommentReactionValidation.cs
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

public static class CommentReactionValidation
{
    private static readonly string[] ValidReactionTypes = { "like", "love", "haha", "wow", "sad", "angry", "care" };

    public static async Task<OperationResult<bool>> ValidateCommentReactionCreationAsync(
        CommentReactionCreateRequestDto dto,
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

        if (string.IsNullOrWhiteSpace(dto.ReactionType))
        {
            return OperationResult<bool>.Failure("ReactionType is required");
        }

        if (!ValidReactionTypes.Contains(dto.ReactionType.ToLower()))
        {
            return OperationResult<bool>.Failure("Invalid reaction type. Must be: like, love, haha, wow, sad, angry, or care");
        }

        // Check if comment exists
        var commentExistenceResult = await commentRepository.IsCommentExistedByCommentID(dto.CommentId);
        if (!commentExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check comment existence for CommentId {CommentId}: {Message}",
                dto.CommentId, commentExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify comment existence");
        }

        if (commentExistenceResult.Data?.CommentExists != true)
        {
            logger.LogWarning("Comment reaction creation attempted for non-existent CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Comment not found or has been deleted");
        }

        logger.LogDebug("Comment reaction creation validation passed for CommentId: {CommentId}, UserId: {UserId}",
            dto.CommentId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }

    public static async Task<OperationResult<bool>> ValidateCommentReactionDeletionAsync(
        CommentReactionDeleteRequestDto dto,
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

        // Check if comment exists
        var commentExistenceResult = await commentRepository.IsCommentExistedByCommentID(dto.CommentId);
        if (!commentExistenceResult.IsSuccess)
        {
            logger.LogWarning("Failed to check comment existence for CommentId {CommentId}: {Message}",
                dto.CommentId, commentExistenceResult.Message);
            return OperationResult<bool>.Failure("Unable to verify comment existence");
        }

        if (commentExistenceResult.Data?.CommentExists != true)
        {
            logger.LogWarning("Comment reaction deletion attempted for non-existent CommentId {CommentId}", dto.CommentId);
            return OperationResult<bool>.Failure("Comment not found or has been deleted");
        }

        logger.LogDebug("Comment reaction deletion validation passed for CommentId: {CommentId}, UserId: {UserId}",
            dto.CommentId, dto.UserId);

        return OperationResult<bool>.Success(true, "Validation passed");
    }
}
