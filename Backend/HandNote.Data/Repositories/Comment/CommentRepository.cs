using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Queries;
using HandNote.Data.Results;
using HandNote.Data.Repositories.Comment.Commands;
using Microsoft.Extensions.Logging;
using HandNote.Data.Repositories.Comment.Queries;

namespace HandNote.Data.Repositories.Comment
{
    public class CommentRepository
    {
        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(ILogger<CommentRepository> logger)
        {
            this._logger = logger;
        }
        public async Task<OperationResult<CommentCreateResponseDto>> AddCommentAsync(CommentCreateRequestDto dto)
        {
            return await AddCommentCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<CommentUpdateResponseDto>> UpdateCommentAsync(CommentUpdateRequestDto dto)
        {
            return await UpdateCommentCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<CommentDeleteResponseDto>> DeleteCommentAsync(CommentDeleteRequestDto dto)
        {
            return await DeleteCommentCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<CommentExistenceResponseDto>> IsCommentExistedByCommentID(int CommentID)
        {
            return await IsCommentExistedByIDQuery.ExecuteAsync(CommentID, _logger);
        }
    }
}