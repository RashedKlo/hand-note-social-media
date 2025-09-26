using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface ICommentReactionRepository
    {
        Task<OperationResult<CommentReactionCreateResponseDto>> CreateCommentReactionAsync(CommentReactionCreateRequestDto dto);
        Task<OperationResult<CommentReactionDeleteResponseDto>> DeleteCommentReactionAsync(CommentReactionDeleteRequestDto dto);

    }
}