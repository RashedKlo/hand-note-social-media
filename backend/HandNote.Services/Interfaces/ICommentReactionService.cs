using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface ICommentReactionService
    {
        Task<OperationResult<CommentReactionCreateResponseDto>> CreateCommentReactionAsync(CommentReactionCreateRequestDto dto);
        Task<OperationResult<CommentReactionDeleteResponseDto>> DeleteCommentReactionAsync(CommentReactionDeleteRequestDto dto);

    }
}