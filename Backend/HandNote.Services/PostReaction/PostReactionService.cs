using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.PostReaction
{
    public sealed class PostReactionService : IPostReactionService
    {
        private readonly IPostReactionRepository _postReactionRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostReactionService> _logger;

        public PostReactionService(
            IPostReactionRepository postReactionRepository,
            IPostRepository postRepository,
            ILogger<PostReactionService> logger)
        {
            _postReactionRepository = postReactionRepository ?? throw new ArgumentNullException(nameof(postReactionRepository));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<PostReactionCreateResponseDto>> CreatePostReactionAsync(PostReactionCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Post reaction creation attempted with null data");
                return OperationResult<PostReactionCreateResponseDto>.Failure("Post reaction data is required");
            }

            _logger.LogInformation("Processing post reaction creation for PostId: {PostId}, UserId: {UserId}, ReactionType: {ReactionType}",
                dto.PostId, dto.UserId, dto.ReactionType);

            // Validate post reaction creation data using stored procedure logic
            var validationResult = await PostReactionValidation.ValidatePostReactionCreationAsync(dto, _postRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Post reaction creation validation failed for PostId {PostId}, UserId {UserId}: {Error}",
                    dto.PostId, dto.UserId, validationResult.Message);
                return OperationResult<PostReactionCreateResponseDto>.Failure(validationResult.Message);
            }

            // Create post reaction via repository (which calls the stored procedure)
            var createResult = await _postReactionRepository.CreatePostReactionAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Post reaction creation failed for PostId {PostId}, UserId {UserId}: {Message}",
                    dto.PostId, dto.UserId, createResult.Message);
                return OperationResult<PostReactionCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Post reaction  successfully - ReactionId: {ReactionId}, PostId: {PostId}, UserId: {UserId}, ReactionType: {ReactionType}",
                 createResult.Data?.ReactionId, dto.PostId, dto.UserId, createResult.Data?.ReactionType);

            return OperationResult<PostReactionCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<PostReactionDeleteResponseDto>> DeletePostReactionAsync(PostReactionDeleteRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Post reaction deletion attempted with null data");
                return OperationResult<PostReactionDeleteResponseDto>.Failure("Post reaction data is required");
            }

            _logger.LogInformation("Processing post reaction deletion for PostId: {PostId}, UserId: {UserId}",
                dto.PostId, dto.UserId);

            // Validate post reaction deletion data using stored procedure logic
            var validationResult = await PostReactionValidation.ValidatePostReactionDeletionAsync(dto, _postRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Post reaction deletion validation failed for PostId {PostId}, UserId {UserId}: {Error}",
                    dto.PostId, dto.UserId, validationResult.Message);
                return OperationResult<PostReactionDeleteResponseDto>.Failure(validationResult.Message);
            }

            // Delete post reaction via repository (which calls the stored procedure)
            var deleteResult = await _postReactionRepository.DeletePostReactionAsync(dto);

            if (!deleteResult.IsSuccess)
            {
                _logger.LogWarning("Post reaction deletion failed for PostId {PostId}, UserId {UserId}: {Message}",
                    dto.PostId, dto.UserId, deleteResult.Message);
                return OperationResult<PostReactionDeleteResponseDto>.Failure(deleteResult.Message);
            }

            _logger.LogInformation("Post reaction deleted successfully - ReactionId: {ReactionId}, PostId: {PostId}, UserId: {UserId}, RemovedReactionType: {RemovedReactionType}",
                deleteResult.Data?.ReactionId, dto.PostId, dto.UserId, deleteResult.Data?.RemovedReactionType);

            return OperationResult<PostReactionDeleteResponseDto>.Success(deleteResult.Data!, deleteResult.Message);
        }

    }


}