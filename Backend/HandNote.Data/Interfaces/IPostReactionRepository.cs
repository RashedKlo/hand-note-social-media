using System;
using System.Collections.Generic;
using System.Linq;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IPostReactionRepository
    {
        Task<OperationResult<PostReactionCreateResponseDto>> CreatePostReactionAsync(PostReactionCreateRequestDto dto);
        Task<OperationResult<PostReactionDeleteResponseDto>> DeletePostReactionAsync(PostReactionDeleteRequestDto dto);

    }
}