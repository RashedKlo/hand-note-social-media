using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using System.ComponentModel.DataAnnotations;
using HandNote.Data.DTOs.Message.RequestBodies;

namespace HandNote.Api.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(
            IMessageService messageService,
            ILogger<MessagesController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Send a message
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<MessageAddResponseDto>>> SendMessage(
            [FromBody] MessageAddRequestDto request)
        {
            _logger.LogInformation("SendMessage request initiated");

            var result = await _messageService.AddMessageAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("SendMessage failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Message sent successfully - ID: {MessageId}",
                result.Data?.MessageId);

            return Ok(new SuccessResponse<MessageAddResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Mark messages as read
        /// </summary>
        [HttpPut("{ConversationId:int}/read")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<MessagesReadResponseDto>>> MarkAsRead(
            [FromRoute] int ConversationId,
            [FromBody] MessagesReadRequestBody request)
        {
            _logger.LogInformation("MarkAsRead request initiated");
            var messageRequest = new MessagesReadRequestDto { ConversationId = ConversationId, UserId = request.UserId };

            var result = await _messageService.MarkMessagesAsReadAsync(messageRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("MarkAsRead failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Messages marked as read successfully - Count: {MessageCount}",
                result.Data?.MessagesMarkedAsRead ?? 0);

            return Ok(new SuccessResponse<MessagesReadResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Get messages
        /// </summary>
        [HttpGet("{ConversationId:int}/search")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<List<MessagesGetResponseDto>>>> GetMessages([FromRoute] int ConversationId,
            [FromBody] MessagesGetRequestBody request,
             [FromQuery, Range(1, 100)] int pageSize = 20,
            [FromQuery, Range(1, int.MaxValue)] int pageNumber = 1)
        {
            _logger.LogInformation("GetMessages request initiated");

            var messageRequest = new MessagesGetRequestDto { ConversationId = ConversationId, CurrentUserId = request.CurrentUserId, PageSize = pageSize, PageNumber = pageNumber };
            var result = await _messageService.GetMessagesAsync(messageRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetMessages failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Messages retrieved successfully - Count: {MessageCount}",
                result.Data?.Count ?? 0);

            return Ok(new SuccessResponse<List<MessagesGetResponseDto>>(result.Data!, result.Message));
        }
    }
}