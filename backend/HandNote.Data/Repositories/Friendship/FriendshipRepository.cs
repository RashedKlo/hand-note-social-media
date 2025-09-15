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
            return await AddFriendCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<FriendshipUpdateResponseDto>> UpdateFriendAsync(FriendshipUpdateRequestDto dto)
        {
            return await UpdateFriendCommand.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<FriendshipExistenceResponseDto>> IsFriendshipExistedByID(FriendshipExistenceRequestDto dto)
        {
            return await IsFriendshipExistedByIDQuery.ExecuteAsync(dto, _logger);
        }
        public async Task<OperationResult<GetUserFriendsResponseDto>> GetUserFriendsAsync(GetUserFriendsRequestDto dto)
        {
            return await GetUserFriendsQuery.ExecuteAsync(dto, _logger);
        }

    }
}