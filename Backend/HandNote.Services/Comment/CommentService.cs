
// CommentService.cs
using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.Comment.Queries;

namespace HandNote.Services.Comment
{
    public sealed class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<CommentService> _logger;

        public CommentService(
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<CommentCreateResponseDto>> AddCommentAsync(CommentCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Comment creation attempted with null data");
                return OperationResult<CommentCreateResponseDto>.Failure("Comment data is required");
            }

            _logger.LogInformation("Processing comment creation for PostId: {PostId}, UserId: {UserId}",
                dto.PostId, dto.UserId);

            var validationResult = await CommentValidation.ValidateCommentCreationAsync(dto, _postRepository, _commentRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Comment creation validation failed for PostId {PostId}, UserId {UserId}: {Error}",
                    dto.PostId, dto.UserId, validationResult.Message);
                return OperationResult<CommentCreateResponseDto>.Failure(validationResult.Message);
            }

            var createResult = await _commentRepository.AddCommentAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Comment creation failed for PostId {PostId}, UserId {UserId}: {Message}",
                    dto.PostId, dto.UserId, createResult.Message);
                return OperationResult<CommentCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Comment created successfully - CommentId: {CommentId}, PostId: {PostId}, UserId: {UserId}",
                createResult.Data?.CommentId, dto.PostId, dto.UserId);

            return OperationResult<CommentCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<CommentUpdateResponseDto>> UpdateCommentAsync(CommentUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Comment update attempted with null data");
                return OperationResult<CommentUpdateResponseDto>.Failure("Comment data is required");
            }

            _logger.LogInformation("Processing comment update for CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

            var validationResult = await CommentValidation.ValidateCommentUpdateAsync(dto, _commentRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Comment update validation failed for CommentId {CommentId}, UserId {UserId}: {Error}",
                    dto.CommentId, dto.UserId, validationResult.Message);
                return OperationResult<CommentUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _commentRepository.UpdateCommentAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("Comment update failed for CommentId {CommentId}, UserId {UserId}: {Message}",
                    dto.CommentId, dto.UserId, updateResult.Message);
                return OperationResult<CommentUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("Comment updated successfully - CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

            return OperationResult<CommentUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }

        public async Task<OperationResult<CommentDeleteResponseDto>> DeleteCommentAsync(CommentDeleteRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Comment deletion attempted with null data");
                return OperationResult<CommentDeleteResponseDto>.Failure("Comment data is required");
            }

            _logger.LogInformation("Processing comment deletion for CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

            var validationResult = await CommentValidation.ValidateCommentDeletionAsync(dto, _commentRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Comment deletion validation failed for CommentId {CommentId}, UserId {UserId}: {Error}",
                    dto.CommentId, dto.UserId, validationResult.Message);
                return OperationResult<CommentDeleteResponseDto>.Failure(validationResult.Message);
            }

            var deleteResult = await _commentRepository.DeleteCommentAsync(dto);

            if (!deleteResult.IsSuccess)
            {
                _logger.LogWarning("Comment deletion failed for CommentId {CommentId}, UserId {UserId}: {Message}",
                    dto.CommentId, dto.UserId, deleteResult.Message);
                return OperationResult<CommentDeleteResponseDto>.Failure(deleteResult.Message);
            }

            _logger.LogInformation("Comment deleted successfully - CommentId: {CommentId}, UserId: {UserId}",
                dto.CommentId, dto.UserId);

            return OperationResult<CommentDeleteResponseDto>.Success(deleteResult.Data!, deleteResult.Message);
        }

        public async Task<OperationResult<CommentExistenceResponseDto>> IsCommentExistedByCommentID(int commentId)
        {
            if (commentId <= 0)
            {
                _logger.LogWarning("Comment existence check attempted with invalid CommentId: {CommentId}", commentId);
                return OperationResult<CommentExistenceResponseDto>.Failure("CommentId must be a positive integer");
            }

            _logger.LogInformation("Processing comment existence check for CommentId: {CommentId}", commentId);

            var existenceResult = await _commentRepository.IsCommentExistedByCommentID(commentId);

            if (!existenceResult.IsSuccess)
            {
                _logger.LogWarning("Comment existence check failed for CommentId {CommentId}: {Message}",
                    commentId, existenceResult.Message);
                return OperationResult<CommentExistenceResponseDto>.Failure(existenceResult.Message);
            }

            _logger.LogInformation("Comment existence check completed successfully - CommentId: {CommentId}, Exists: {CommentExists}, IsDeleted: {IsDeleted}",
                commentId, existenceResult.Data?.CommentExists, existenceResult.Data?.IsDeleted);

            return OperationResult<CommentExistenceResponseDto>.Success(existenceResult.Data!, existenceResult.Message);
        }
    }
}