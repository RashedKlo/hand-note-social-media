
// IFriendshipService.cs
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<OperationResult<FriendshipCreateResponseDto>> SendFriendRequestAsync(FriendshipCreateRequestDto dto);
        Task<OperationResult<FriendshipUpdateResponseDto>> AcceptFriendRequestAsync(FriendshipUpdateRequestDto dto);
        Task<OperationResult<FriendshipUpdateResponseDto>> DeclineFriendRequestAsync(FriendshipUpdateRequestDto dto);
        Task<OperationResult<FriendshipUpdateResponseDto>> CancelFriendRequestAsync(FriendshipUpdateRequestDto dto);
        Task<OperationResult<FriendshipUpdateResponseDto>> BlockUserAsync(FriendshipUpdateRequestDto dto);
        Task<OperationResult<FriendshipExistenceResponseDto>> IsFriendshipExistedByIDAsync(FriendshipExistenceRequestDto dto);
        Task<OperationResult<GetUserFriendsResponseDto>> GetUserFriendsAsync(GetUserFriendsRequestDto dto);
    }
}