using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.DTOs.Post.Queries;

namespace HandNote.Services.Post
{
    public sealed class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository, ILogger<PostService> logger)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<PostCreateResponseDto>> CreatePostAsync(PostCreateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Post creation attempted with null data");
                return OperationResult<PostCreateResponseDto>.Failure("Post data is required");
            }

            _logger.LogInformation("Processing post creation for UserId: {UserId}, PostType: {PostType}",
                dto.UserId, dto.PostType);

            // Validate post creation data using stored procedure logic
            var validationResult = await PostValidation.ValidatePostCreationAsync(dto, _postRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Post creation validation failed for UserId {UserId}: {Error}",
                    dto.UserId, validationResult.Message);
                return OperationResult<PostCreateResponseDto>.Failure(validationResult.Message);
            }

            // Create post via repository (which calls the stored procedure)
            var createResult = await _postRepository.CreatePostAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("Post creation failed for UserId {UserId}: {Message}",
                    dto.UserId, createResult.Message);
                return OperationResult<PostCreateResponseDto>.Failure(createResult.Message);
            }

            _logger.LogInformation("Post created successfully - PostId: {PostId}, UserId: {UserId}",
                createResult.Data?.Post!.PostId, dto.UserId);

            return OperationResult<PostCreateResponseDto>.Success(createResult.Data!, createResult.Message);
        }
        public async Task<OperationResult<PostDeleteResponseDto>> DeletePostAsync(PostDeleteRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Post deletion attempted with null data");
                return OperationResult<PostDeleteResponseDto>.Failure("Post data is required");
            }

            _logger.LogInformation("Processing post deletion for PostId: {PostId}, UserId: {UserId}",
                dto.PostId, dto.UserId);

            // Validate post deletion data using stored procedure logic
            var validationResult = await PostValidation.ValidatePostDeletionAsync(dto, _postRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Post deletion validation failed for PostId {PostId}, UserId {UserId}: {Error}",
                    dto.PostId, dto.UserId, validationResult.Message);
                return OperationResult<PostDeleteResponseDto>.Failure(validationResult.Message);
            }

            // Delete post via repository (which calls the stored procedure)
            var deleteResult = await _postRepository.DeletePostAsync(dto);

            if (!deleteResult.IsSuccess)
            {
                _logger.LogWarning("Post deletion failed for PostId {PostId}, UserId {UserId}: {Message}",
                    dto.PostId, dto.UserId, deleteResult.Message);
                return OperationResult<PostDeleteResponseDto>.Failure(deleteResult.Message);
            }

            _logger.LogInformation("Post deleted successfully - PostId: {PostId}, UserId: {UserId}, DeletedRows: {DeletedRows}",
                dto.PostId, dto.UserId, deleteResult.Data?.DeletedRows);

            return OperationResult<PostDeleteResponseDto>.Success(deleteResult.Data!, deleteResult.Message);
        }
        public async Task<OperationResult<PostExistenceResponseDto>> IsPostExistedAsync(int PostId)
        {
            if (PostId < 0)
            {
                _logger.LogWarning("Post existence check attempted with null data");
                return OperationResult<PostExistenceResponseDto>.Failure("Post data is required");
            }

            _logger.LogInformation("Processing post existence check for PostId: {PostId}", PostId);



            // Check post existence via repository (which calls the stored procedure)
            var existenceResult = await _postRepository.IsPostExistedById(PostId);

            if (!existenceResult.IsSuccess)
            {
                _logger.LogWarning("Post existence check failed for PostId {PostId}: {Message}",
                    PostId, existenceResult.Message);
                return OperationResult<PostExistenceResponseDto>.Failure(existenceResult.Message);
            }

            _logger.LogInformation("Post existence check completed successfully - PostId: {PostId}, Exists: {Exists}",
                PostId, existenceResult.Data?.Exists);

            return OperationResult<PostExistenceResponseDto>.Success(existenceResult.Data!, existenceResult.Message);
        }





    }
}