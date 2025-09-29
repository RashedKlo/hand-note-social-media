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
        /// Get user's friends list
        /// </summary>
        [HttpGet("{UserId}/friends")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<List<UserFriendsGetResponseDto>>>> GetUserFriends(
            [FromRoute] int UserId)
        {
            _logger.LogInformation("GetUserFriends initiated for UserId: {UserId}", UserId);

            var request = new UserFriendsGetRequestDto { UserId = UserId };
            var result = await _friendshipService.GetUserFriendsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetUserFriends failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User friends retrieved successfully - Count: {FriendsCount}",
                result.Data?.Count ?? 0);

            return Ok(new SuccessResponse<List<UserFriendsGetResponseDto>>(result.Data!, result.Message));
        }
    }
}