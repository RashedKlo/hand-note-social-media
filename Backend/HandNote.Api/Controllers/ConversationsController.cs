using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;

namespace HandNote.Api.Controllers
{
    [Route("api/conversations")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ILogger<ConversationsController> _logger;

        public ConversationsController(
            IConversationRepository conversationRepository,
            ILogger<ConversationsController> logger)
        {
            _conversationRepository = conversationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new conversation
        /// </summary>
        /// <param name="request">Conversation creation details</param>
        /// <returns>Created conversation</returns>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<ConversationCreateResponseDto>>> CreateConversation(
            [FromBody] ConversationCreateRequestDto request)
        {
            _logger.LogInformation("CreateConversation request initiated by UserId: {UserId}",
                request.CurrentUserId);

            var result = await _conversationRepository.CreateConversationAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreateConversation failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Conversation created successfully - ID: {ConversationId}",
                result.Data?.ConversationId);

            return Ok(new SuccessResponse<ConversationCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Check if a user is part of a specific conversation
        /// </summary>
        /// <param name="conversationId">The ID of the conversation</param>
        /// <param name="userId">The ID of the user to check</param>
        /// <returns>Conversation membership information</returns>
        [HttpGet("{conversationId}/users/{userId}/membership")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<ConversationExistenceResponseDto>>> CheckUserMembership(
            [FromRoute] int conversationId,
            [FromRoute] int userId)
        {
            var request = new ConversationExistenceRequestDto
            {
                ConversationId = conversationId,
                UserId = userId
            };

            _logger.LogInformation("CheckUserMembership request for ConversationId: {ConversationId}, UserId: {UserId}",
                conversationId, userId);

            var result = await _conversationRepository.IsUserPartOfConversationAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CheckUserMembership failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User membership check completed - ConversationId: {ConversationId}, UserId: {UserId}, IsMember: {IsMember}",
                conversationId, userId, result.Data?.IsUserPartOfConversation);

            return Ok(new SuccessResponse<ConversationExistenceResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Alternative endpoint: Check if a user is part of a conversation using request body
        /// </summary>
        /// <param name="request">Conversation existence check details</param>
        /// <returns>Conversation membership information</returns>
        [HttpPost("membership/check")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<ConversationExistenceResponseDto>>> CheckUserMembershipByBody(
            [FromBody] ConversationExistenceRequestDto request)
        {
            _logger.LogInformation("CheckUserMembershipByBody request for ConversationId: {ConversationId}, UserId: {UserId}",
                request.ConversationId, request.UserId);

            var result = await _conversationRepository.IsUserPartOfConversationAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CheckUserMembershipByBody failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User membership check completed - ConversationId: {ConversationId}, UserId: {UserId}, IsMember: {IsMember}",
                request.ConversationId, request.UserId, result.Data?.IsUserPartOfConversation);

            return Ok(new SuccessResponse<ConversationExistenceResponseDto>(result.Data!, result.Message));
        }
    }
}