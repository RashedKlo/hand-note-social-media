using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<OperationResult<FriendshipCreateResponseDto>> AddFriendAsync(FriendshipCreateRequestDto dto);
        Task<OperationResult<FriendshipUpdateResponseDto>> UpdateFriendAsync(FriendshipUpdateRequestDto dto);
        Task<OperationResult<FriendshipExistenceResponseDto>> IsFriendshipExistedByID(FriendshipExistenceRequestDto dto);
        Task<OperationResult<GetUserFriendsResponseDto>> GetUserFriendsAsync(GetUserFriendsRequestDto dto);
    }
}