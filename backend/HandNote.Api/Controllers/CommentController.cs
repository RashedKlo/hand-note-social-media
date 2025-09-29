using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.DTOs.Comment.Queries;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.Comment.RequestBodies;

namespace HandNote.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(
            ICommentRepository commentRepository,
            ILogger<CommentsController> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="request">Comment creation details</param>
        /// <returns>Created comment</returns>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentCreateResponseDto>>> CreateComment(
            [FromBody] CommentCreateRequestDto request)
        {
            _logger.LogInformation("CreateComment request for UserId: {UserId}", request.UserId);

            var result = await _commentRepository.AddCommentAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreateComment failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment created successfully - ID: {CommentId}",
                result.Data?.CommentId);

            return Ok(new SuccessResponse<CommentCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Update an existing comment
        /// </summary>
        /// <param name="commentId">The ID of the comment to update</param>
        /// <param name="request">Comment update details</param>
        /// <returns>Updated comment</returns>
        [HttpPut("{commentId}")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentUpdateResponseDto>>> UpdateComment(
            [FromRoute] int commentId,
            [FromBody] CommentUpdateRequestBody request)
        {
            var commentRequest = new CommentUpdateRequestDto
            {
                UserId = request.UserId,
                Content = request.Content,
                MediaId = request.MediaId
            };
            _logger.LogInformation("UpdateComment request for CommentId: {CommentId}", commentId);

            var result = await _commentRepository.UpdateCommentAsync(commentRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("UpdateComment failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment updated successfully - ID: {CommentId}", commentId);

            return Ok(new SuccessResponse<CommentUpdateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete</param>
        /// <param name="request">Comment deletion details</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{commentId}")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentDeleteResponseDto>>> DeleteComment(
            [FromRoute] int commentId,
            [FromBody] CommentDeleteRequestBody request)
        {
            var commentRequest = new CommentDeleteRequestDto { CommentId = commentId, UserId = request.UserId };

            _logger.LogInformation("DeleteComment request for CommentId: {CommentId}", commentId);

            var result = await _commentRepository.DeleteCommentAsync(commentRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("DeleteComment failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment deleted successfully - ID: {CommentId}", commentId);

            return Ok(new SuccessResponse<CommentDeleteResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Check if a comment exists
        /// </summary>
        /// <param name="commentId">The ID of the comment to check</param>
        /// <returns>Comment existence result</returns>
        [HttpGet("{commentId}/exists")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<CommentExistenceResponseDto>>> CheckCommentExists(
            [FromRoute] int commentId)
        {
            _logger.LogInformation("CheckCommentExists request for CommentId: {CommentId}", commentId);

            var result = await _commentRepository.IsCommentExistedByCommentID(commentId);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CheckCommentExists failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Comment existence check completed - CommentId: {CommentId}, Exists: {Exists}",
                commentId, result.Data?.CommentExists);

            return Ok(new SuccessResponse<CommentExistenceResponseDto>(result.Data!, result.Message));
        }
    }
}