using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.DTOs.Post.Queries;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.Post.Commands;
using HandNote.Data.Repositories.Post.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Post
{
    public class PostRepository : IPostRepository
    {
        private ILogger<PostRepository> _logger;
        public PostRepository(ILogger<PostRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<OperationResult<PostCreateResponseDto>> CreatePostAsync(PostCreateRequestDto dto)
        {
            return await CreatePostCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<PostDeleteResponseDto>> DeletePostAsync(PostDeleteRequestDto dto)
        {
            return await DeletePostCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<PostExistenceResponseDto>> IsPostExistedById(int PostId)
        {
            return await IsPostExistedQuery.ExecuteAsync(PostId, _logger);
        }
    }
}