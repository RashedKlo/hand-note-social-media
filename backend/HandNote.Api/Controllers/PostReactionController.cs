using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.PostReaction.RequestBodies;

namespace HandNote.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostReactionController : ControllerBase
    {
        private readonly IPostReactionService _postReactionService;
        private readonly ILogger<PostReactionController> _logger;

        public PostReactionController(
            IPostReactionService postReactionService,
            ILogger<PostReactionController> logger)
        {
            _postReactionService = postReactionService;
            _logger = logger;
        }

        /// <summary>
        /// Create a reaction to a post
        /// </summary>
        [HttpPost("reactions")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<PostReactionCreateResponseDto>>> CreatePostReaction(
            [FromBody] PostReactionCreateRequestDto request)
        {
            _logger.LogInformation("CreatePostReaction request initiated for PostId: {PostId}, UserId: {UserId}",
                request.PostId, request.UserId);

            var result = await _postReactionService.CreatePostReactionAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreatePostReaction failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Post reaction created successfully - ID: {PostReactionId}",
                result.Data?.ReactionId);

            return Ok(new SuccessResponse<PostReactionCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Delete a reaction from a post
        /// </summary>
        [HttpDelete("{postId:int}/reactions")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<PostReactionDeleteResponseDto>>> DeletePostReaction(
            [FromRoute] int postId,
            [FromBody] PostReactionDeleteRequestBody request)
        {
            var reactionRequest = new PostReactionDeleteRequestDto { PostId = postId, UserId = request.UserId };

            _logger.LogInformation("DeletePostReaction request initiated for PostId: {PostId}, UserId: {UserId}",
                postId, request.UserId);

            var result = await _postReactionService.DeletePostReactionAsync(reactionRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("DeletePostReaction failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Post reaction deleted successfully - PostId: {PostId}, UserId: {UserId}",
                postId, request.UserId);

            return Ok(new SuccessResponse<PostReactionDeleteResponseDto>(result.Data!, result.Message));
        }
    }
}