using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Post.Create;
using HandNote.Data.DTOs.Post.Delete;
using HandNote.Data.DTOs.Post.Queries;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.Post.RequestBodies;

namespace HandNote.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            IPostService postService,
            ILogger<PostsController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<PostCreateResponseDto>>> CreatePost(
            [FromBody] PostCreateRequestDto request)
        {
            _logger.LogInformation("CreatePost request initiated for UserId: {UserId}", request.UserId);

            var result = await _postService.CreatePostAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreatePost failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Post created successfully - ID: {PostId}",
                result.Data?.Post?.PostId);

            return Ok(new SuccessResponse<PostCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        [HttpDelete("{postId:int}")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<PostDeleteResponseDto>>> DeletePost(
            [FromRoute] int postId,
            [FromBody] PostDeleteRequestBody request)
        {
            var postRequest = new PostDeleteRequestDto { PostId = postId, UserId = request.UserId };

            _logger.LogInformation("DeletePost request initiated for PostId: {PostId}", postId);

            var result = await _postService.DeletePostAsync(postRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("DeletePost failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Post deleted successfully - ID: {PostId}", postId);

            return Ok(new SuccessResponse<PostDeleteResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Check if a post exists
        /// </summary>
        [HttpGet("{postId:int}/exists")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<PostExistenceResponseDto>>> CheckPostExists(
            [FromRoute] int postId)
        {
            _logger.LogInformation("CheckPostExists request initiated for PostId: {PostId}", postId);

            var result = await _postService.IsPostExistedAsync(postId);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CheckPostExists failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Post existence check completed - PostId: {PostId}, Exists: {Exists}",
                postId, result.Data?.Exists);

            return Ok(new SuccessResponse<PostExistenceResponseDto>(result.Data!, result.Message));
        }
    }
}