using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.CommentReaction.RequestBodies;

namespace HandNote.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentReactionController : ControllerBase
    {
        private readonly ICommentReactionService _commentReactionService;
        private readonly ILogger<CommentReactionController> _logger;

        public CommentReactionController(
            ICommentReactionService commentReactionService,
            ILogger<CommentReactionController> logger)
        {
            _commentReactionService = commentReactionService;
            _logger = logger;
        }

        /// <summary>
        /// Create a reaction to a comment
        /// </summary>
        /// <param name="request">Comment reaction details</param>
        /// <returns>Created comment reaction</returns>
        [HttpPost("reactions")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentReactionCreateResponseDto>>> CreateCommentReaction(
            [FromBody] CommentReactionCreateRequestDto request)
        {
            _logger.LogInformation("CreateCommentReaction request for CommentId: {CommentId}, UserId: {UserId}",
                request.CommentId, request.UserId);

            var result = await _commentReactionService.CreateCommentReactionAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreateCommentReaction failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment reaction created successfully - ID: {CommentReactionId}",
                result.Data?.ReactionId);

            return Ok(new SuccessResponse<CommentReactionCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Delete a reaction from a comment
        /// </summary>
        /// <param name="commentId">The ID of the comment</param>
        /// <param name="request">User details for the reaction to delete</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{commentId}/reactions")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentReactionDeleteResponseDto>>> DeleteCommentReaction(
            [FromRoute] int commentId,
            [FromBody] CommentReactionDeleteRequestBody request)
        {

            var commentRequest = new CommentReactionDeleteRequestDto { UserId = request.UserId, CommentId = commentId };
            _logger.LogInformation("DeleteCommentReaction request for CommentId: {CommentId}, UserId: {UserId}",
                commentId, request.UserId);

            var result = await _commentReactionService.DeleteCommentReactionAsync(commentRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("DeleteCommentReaction failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment reaction deleted successfully - CommentId: {CommentId}, UserId: {UserId}",
                commentId, request.UserId);

            return Ok(new SuccessResponse<CommentReactionDeleteResponseDto>(result.Data!, result.Message));
        }
    }
}