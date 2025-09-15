using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Comment.Queries;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface ICommentRepository
    {
        Task<OperationResult<CommentCreateResponseDto>> AddCommentAsync(CommentCreateRequestDto dto);
        Task<OperationResult<CommentUpdateResponseDto>> UpdateCommentAsync(CommentUpdateRequestDto dto);
        Task<OperationResult<CommentDeleteResponseDto>> DeleteCommentAsync(CommentDeleteRequestDto dto);
        Task<OperationResult<CommentExistenceResponseDto>> IsCommentExistedByCommentID(int CommentID);
    }
}