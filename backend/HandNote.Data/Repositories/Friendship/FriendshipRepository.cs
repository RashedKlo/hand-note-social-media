using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Data.Repositories.Friendship.Commands;
using Microsoft.Extensions.Logging;
using HandNote.Data.Repositories.Friendship.Queries;

namespace HandNote.Data.Repositories.Friendship
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ILogger<FriendshipRepository> _logger;
        public FriendshipRepository(ILogger<FriendshipRepository> logger)
        {
            this._logger = logger;
        }
public async Task<OperationResult<FriendshipCreateResponseDto>> AddFriendAsync(FriendshipCreateRequestDto dto)
{
    _logger.LogInformation("Repository: Adding friend. RequesterId: {RequesterId}, AddresseeId: {AddresseeId}", dto.RequesterId, dto.AddresseeId);
    return await AddFriendCommand.ExecuteAsync(dto, _logger);
}

public async Task<OperationResult<FriendshipUpdateResponseDto>> UpdateFriendAsync(FriendshipUpdateRequestDto dto)
{
    _logger.LogInformation("Repository: Updating friendship. FriendshipId: {FriendshipId}, Status: {Status}", dto.FriendshipId, dto.NewStatus);
    return await UpdateFriendCommand.ExecuteAsync(dto, _logger);
}

public async Task<OperationResult<FriendshipExistenceResponseDto>> IsFriendshipExistedByID(FriendshipExistenceRequestDto dto)
{
    _logger.LogInformation("Repository: Checking if friendship exists. UserId1: {UserId1}, UserId2: {UserId2}", dto.UserId1, dto.UserId2);
    return await IsFriendshipExistedByIDQuery.ExecuteAsync(dto, _logger);
}

          // New query methods
        public async Task<OperationResult<GetFriendRequestsResponseDto>> GetFriendRequestsAsync(GetFriendRequestsRequestDto dto)
        {
            _logger.LogInformation("Repository: Getting friend requests for UserId: {UserId}", dto.UserId);
            return await GetFriendRequestsQuery.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<GetUserFriendsResponseDto>> GetUserFriendsAsync(GetUserFriendsRequestDto dto)
        {
            _logger.LogInformation("Repository: Getting user friends for UserId: {UserId}", dto.UserId);
            return await GetUserFriendsQuery.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<SearchUserFriendsResponseDto>> SearchUserFriendsAsync(SearchUserFriendsRequestDto dto)
        {
            _logger.LogInformation("Repository: Searching user friends for UserId: {UserId}, Filter: {Filter}", 
                dto.UserId, dto.Filter);
            return await SearchUserFriendsQuery.ExecuteAsync(dto, _logger);
        }


    }
}