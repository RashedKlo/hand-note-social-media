using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.PostReaction.Commands;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.PostReaction
{
    public class PostReactionRepository : IPostReactionRepository
    {
        private readonly ILogger<PostReactionRepository> _logger;
        public PostReactionRepository(ILogger<PostReactionRepository> logger)
        {
            this._logger = logger;
        }
        public async Task<OperationResult<PostReactionCreateResponseDto>> CreatePostReactionAsync(PostReactionCreateRequestDto dto)
        {
            return await CreatePostReactionCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<PostReactionDeleteResponseDto>> DeletePostReactionAsync(PostReactionDeleteRequestDto dto)
        {
            return await DeletePostReactionCommand.ExecuteAsync(dto, _logger);
        }


    }
}