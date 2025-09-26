using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;

namespace HandNote.Services.CommentReaction
{
    public sealed class CommentReactionService : ICommentReactionService
    {
        private readonly ICommentReactionRepository _commentReactionRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentReactionService> _logger;

        public CommentReactionService(
            ICommentReactionRepository commentReactionRepository,
            ICommentRepository commentRepository,
            ILogger<CommentReactionService> logger)
        {
            _commentReactionRepository = commentReactionRepository ?? throw new ArgumentNullException(nameof(commentReactionRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<CommentReactionCreateResponseDto>> CreateCommentReactionAsync(CommentReactionCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Comment reaction creation attempted with null data");
                return OperationResult<CommentReactionCreateResponseDto>.Failure("Comment reaction data is required");
            }

            _logger.LogInformation("Processing comment reaction creation for CommentId: {CommentId}, UserId: {UserId}, ReactionType: {ReactionType}",
                dto.CommentId, dto.UserId, dto.ReactionType);

            var validationResult = await CommentReactionValidation.ValidateCommentReactionCreationAsync(dto, _commentRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Comment reaction creation validation failed for CommentId {CommentId}, UserId {UserId}: {Error}",
                    dto.CommentId, dto.UserId, validationResult.Message);
                return OperationResult<CommentReactionCreateResponseDto>.Failure(validationResult.Message);
            }

            var createResult = await _commentReactionRepository.CreateCommentReactionAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Comment reaction creation failed for CommentId {CommentId}, UserId {UserId}: {Message}",
                    dto.CommentId, dto.UserId, createResult.Message);
                return OperationResult<CommentReactionCreateResponseDto>.Failure(createResult.Message);
            }

            var actionType = createResult.Data?.IsUpdate == true ? "updated" : "created";
            _logger.LogInformation("Comment reaction {ActionType} successfully - ReactionId: {ReactionId}, CommentId: {CommentId}, UserId: {UserId}, ReactionType: {ReactionType}",
                actionType, createResult.Data?.ReactionId, dto.CommentId, dto.UserId, createResult.Data?.ReactionType);

            return OperationResult<CommentReactionCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<CommentReactionDeleteResponseDto>> DeleteCommentReactionAsync(CommentReactionDeleteRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Comment reaction deletion attempted with null data");
                return OperationResult<CommentReactionDeleteResponseDto>.Failure("Comment reaction data is required");
            }

            _logger.LogInformation("Processing comment reaction deletion for CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

            var validationResult = await CommentReactionValidation.ValidateCommentReactionDeletionAsync(dto, _commentRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Comment reaction deletion validation failed for CommentId {CommentId}, UserId {UserId}: {Error}",
                    dto.CommentId, dto.UserId, validationResult.Message);
                return OperationResult<CommentReactionDeleteResponseDto>.Failure(validationResult.Message);
            }

            var deleteResult = await _commentReactionRepository.DeleteCommentReactionAsync(dto);

            if (!deleteResult.IsSuccess)
            {
                _logger.LogWarning("Comment reaction deletion failed for CommentId {CommentId}, UserId {UserId}: {Message}",
                    dto.CommentId, dto.UserId, deleteResult.Message);
                return OperationResult<CommentReactionDeleteResponseDto>.Failure(deleteResult.Message);
            }

            _logger.LogInformation("Comment reaction deleted successfully - ReactionId: {ReactionId}, CommentId: {CommentId}, UserId: {UserId}, RemovedReactionType: {RemovedReactionType}",
                deleteResult.Data?.ReactionId, dto.CommentId, dto.UserId, deleteResult.Data?.RemovedReactionType);

            return OperationResult<CommentReactionDeleteResponseDto>.Success(deleteResult.Data!, deleteResult.Message);
        }

    }
}
