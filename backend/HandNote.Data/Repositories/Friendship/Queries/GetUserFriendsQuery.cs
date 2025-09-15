using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.Friendship.Queries
{
    public class GetUserFriendsQuery
    {
        public static async Task<OperationResult<GetUserFriendsResponseDto>> ExecuteAsync(GetUserFriendsRequestDto dto, ILogger logger)
        {

        }
    }
}