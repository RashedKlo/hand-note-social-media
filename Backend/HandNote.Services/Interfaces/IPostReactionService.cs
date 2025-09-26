using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IPostReactionService
    {
        Task<OperationResult<PostReactionCreateResponseDto>> CreatePostReactionAsync(PostReactionCreateRequestDto dto);
        Task<OperationResult<PostReactionDeleteResponseDto>> DeletePostReactionAsync(PostReactionDeleteRequestDto dto);
    }
}