using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.CommentReaction.Commands;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.CommentReaction
{
    public class CommentReactionRepository : ICommentReactionRepository
    {
        ILogger<CommentReactionRepository> _logger;
        public CommentReactionRepository(ILogger<CommentReactionRepository> logger)
        {
            this._logger = logger;
        }
        public async Task<OperationResult<CommentReactionCreateResponseDto>> CreateCommentReactionAsync(CommentReactionCreateRequestDto dto)
        {
            return await CreateCommentReactionCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<CommentReactionDeleteResponseDto>> DeleteCommentReactionAsync(CommentReactionDeleteRequestDto dto)
        {
            return await DeleteCommentReactionCommand.ExecuteAsync(dto, _logger);
        }

    }
}