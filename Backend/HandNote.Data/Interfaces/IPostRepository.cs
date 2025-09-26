using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.DTOs.Post.Queries;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IPostRepository
    {
        Task<OperationResult<PostCreateResponseDto>> CreatePostAsync(PostCreateRequestDto dto);
        Task<OperationResult<PostDeleteResponseDto>> DeletePostAsync(PostDeleteRequestDto dto);
        Task<OperationResult<PostExistenceResponseDto>> IsPostExistedById(int PostId);

    }
}