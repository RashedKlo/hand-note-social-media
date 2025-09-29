using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Friendship.Create;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.Friendship.RequestBodies;
using HandNote.Data.Models;

namespace HandNote.Api.Controllers
{
    [Route("api/friendships")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ILogger<FriendshipsController> _logger;

        public FriendshipsController(
            IFriendshipService friendshipService,
            ILogger<FriendshipsController> logger)
        {
            _friendshipService = friendshipService;
            _logger = logger;
        }

        /// <summary>
        /// Send friend request
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipCreateResponseDto>>> SendFriendRequest(
            [FromBody] FriendshipCreateRequestDto request)
        {
            _logger.LogInformation("SendFriendRequest initiated");

            var result = await _friendshipService.SendFriendRequestAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("SendFriendRequest failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friend request sent successfully - ID: {FriendshipId}",
                result.Data?.FriendshipId);

            return Ok(new SuccessResponse<FriendshipCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Accept friend request
        /// </summary>
        [HttpPut("{FriendshipId}/accept")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipUpdateResponseDto>>> AcceptFriendRequest(
            [FromRoute] int FriendshipId,
            [FromBody] FriendshipUpdateRequestBody request)
        {
            _logger.LogInformation("AcceptFriendRequest initiated for FriendshipId: {FriendshipId}", FriendshipId);

            var friendshipRequestDto = new FriendshipUpdateRequestDto
            {
                UserId = request.UserId,
                FriendshipId = FriendshipId,
                NewStatus = EnFriendshipStatus.Accepted
            };

            var result = await _friendshipService.AcceptFriendRequestAsync(friendshipRequestDto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("AcceptFriendRequest failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friend request accepted successfully");

            return Ok(new SuccessResponse<FriendshipUpdateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Decline friend request
        /// </summary>
        [HttpPut("{FriendshipId}/decline")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipUpdateResponseDto>>> DeclineFriendRequest(
            [FromRoute] int FriendshipId,
            [FromBody] FriendshipUpdateRequestBody request)
        {
            _logger.LogInformation("DeclineFriendRequest initiated for FriendshipId: {FriendshipId}", FriendshipId);

            var friendshipRequestDto = new FriendshipUpdateRequestDto
            {
                UserId = request.UserId,
                FriendshipId = FriendshipId,
                NewStatus = EnFriendshipStatus.Declined
            };

            var result = await _friendshipService.DeclineFriendRequestAsync(friendshipRequestDto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("DeclineFriendRequest failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friend request declined successfully");

            return Ok(new SuccessResponse<FriendshipUpdateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Cancel friend request
        /// </summary>
        [HttpPut("{FriendshipId}/cancel")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipUpdateResponseDto>>> CancelFriendRequest(
            [FromRoute] int FriendshipId,
            [FromBody] FriendshipUpdateRequestBody request)
        {
            _logger.LogInformation("CancelFriendRequest initiated for FriendshipId: {FriendshipId}", FriendshipId);

            var friendshipRequestDto = new FriendshipUpdateRequestDto
            {
                UserId = request.UserId,
                FriendshipId = FriendshipId,
                NewStatus = EnFriendshipStatus.Cancelled
            };

            var result = await _friendshipService.CancelFriendRequestAsync(friendshipRequestDto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CancelFriendRequest failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friend request cancelled successfully");

            return Ok(new SuccessResponse<FriendshipUpdateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Block user
        /// </summary>
        [HttpPut("{FriendshipId}/block")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipUpdateResponseDto>>> BlockUser(
            [FromRoute] int FriendshipId,
            [FromBody] FriendshipUpdateRequestBody request)
        {
            _logger.LogInformation("BlockUser initiated for FriendshipId: {FriendshipId}", FriendshipId);

            var friendshipRequestDto = new FriendshipUpdateRequestDto
            {
                UserId = request.UserId,
                FriendshipId = FriendshipId,
                NewStatus = EnFriendshipStatus.Blocked
            };

            var result = await _friendshipService.BlockUserAsync(friendshipRequestDto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("BlockUser failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User blocked successfully");

            return Ok(new SuccessResponse<FriendshipUpdateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Check if friendship exists
        /// </summary>
        [HttpPost("check")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<FriendshipExistenceResponseDto>>> CheckFriendship(
            [FromBody] FriendshipExistenceRequestDto request)
        {
            _logger.LogInformation("CheckFriendship initiated");

            var result = await _friendshipService.IsFriendshipExistedByIDAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CheckFriendship failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friendship check completed successfully");

            return Ok(new SuccessResponse<FriendshipExistenceResponseDto>(result.Data!, result.Message));
        }
 /// <summary>
        /// Get pending friend requests for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="limit">Items per page (default: 10, max: 100)</param>
        /// <returns>List of pending friend requests</returns>
        [HttpGet("{userId}/requests")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<GetFriendRequestsResponseDto>>> GetFriendRequests(
            [FromRoute] int userId,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            _logger.LogInformation("GetFriendRequests request for UserId: {UserId}, Page: {Page}, Limit: {Limit}",
                userId, page, limit);

            var request = new GetFriendRequestsRequestDto
            {
                UserId = userId,
                Page = page,
                Limit = limit
            };

            var result = await _friendshipService.GetFriendRequestsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetFriendRequests failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Friend requests retrieved successfully - UserId: {UserId}, Count: {Count}",
                userId, result.Data?.FriendRequests?.Count ?? 0);

            return Ok(new SuccessResponse<GetFriendRequestsResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Get all friends for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="limit">Items per page (default: 10, max: 100)</param>
        /// <returns>List of friends</returns>
        [HttpGet("{userId}/friends")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<GetUserFriendsResponseDto>>> GetUserFriends(
            [FromRoute] int userId,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            _logger.LogInformation("GetUserFriends request for UserId: {UserId}, Page: {Page}, Limit: {Limit}",
                userId, page, limit);

            var request = new GetUserFriendsRequestDto
            {
                UserId = userId,
                Page = page,
                Limit = limit
            };

            var result = await _friendshipService.GetUserFriendsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetUserFriends failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User friends retrieved successfully - UserId: {UserId}, Count: {Count}",
                userId, result.Data?.Friends?.Count ?? 0);

            return Ok(new SuccessResponse<GetUserFriendsResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Search friends by name
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="filter">Search filter (name)</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="limit">Items per page (default: 10, max: 100)</param>
        /// <returns>List of matching friends</returns>
        [HttpGet("{userId}/friends/search")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<SearchUserFriendsResponseDto>>> SearchUserFriends(
            [FromRoute] int userId,
            [FromQuery] string? filter = null,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            _logger.LogInformation("SearchUserFriends request for UserId: {UserId}, Filter: {Filter}, Page: {Page}, Limit: {Limit}",
                userId, filter, page, limit);

            var request = new SearchUserFriendsRequestDto
            {
                UserId = userId,
                Filter = filter,
                Page = page,
                Limit = limit
            };

            var result = await _friendshipService.SearchUserFriendsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("SearchUserFriends failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User friends search completed successfully - UserId: {UserId}, Count: {Count}",
                userId, result.Data?.Friends?.Count ?? 0);

            return Ok(new SuccessResponse<SearchUserFriendsResponseDto>(result.Data!, result.Message));
        }
    
    }
}