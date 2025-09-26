using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HandNote.Api.Responses;
using HandNote.Data.DTOs.Notification;
using HandNote.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HandNote.Data.DTOs.Notification.Create;
using HandNote.Data.DTOs.Notification.Get;
using HandNote.Data.DTOs.Notification.RequestBodies;
using HandNote.Api.Helpers;

namespace HandNote.Api.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Produces("application/json")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(
            INotificationService notificationService,
            ILogger<NotificationsController> logger)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Creates a new notification following MyRashedDesign pattern
        /// </summary>
        [HttpPost]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<NotificationCreateResponseDto>>> CreateNotification(
            [FromBody] NotificationCreateRequestDto request)
        {

            _logger.LogInformation("CreateNotification request for RecipientId: {RecipientId}", request.RecipientId);

            var result = await _notificationService.CreateNotificationAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("CreateNotification failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Notification created successfully - ID: {NotificationId}", result.Data?.NotificationId);

            return Ok(new SuccessResponse<NotificationCreateResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Gets user notifications with high-performance caching
        /// http://localhost:5063/api/auth/notifications/users/userId/search
        /// </summary>
        [HttpGet("users/{userId:int}")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<List<NotificationsGetResponseDto>>>> GetUserNotifications(
            [FromRoute] int userId,
            [FromQuery, Range(1, 100)] int pageSize = 20,
            [FromQuery, Range(1, int.MaxValue)] int pageNumber = 1,
            [FromQuery] bool includeRead = true)
        {

            _logger.LogInformation("GetUserNotifications for UserId: {UserId}, Page: {PageNumber}, Size: {PageSize}",
                userId, pageNumber, pageSize);

            var request = new NotificationsGetRequestDto
            {
                UserId = userId,
                PageSize = pageSize,
                PageNumber = pageNumber,
                IncludeRead = includeRead
            };

            var result = await _notificationService.GetUserNotificationsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetUserNotifications failed for UserId {UserId}: {Message}", userId, result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Retrieved {Count} notifications for UserId: {UserId}", result.Data?.Count ?? 0, userId);

            return Ok(new SuccessResponse<List<NotificationsGetResponseDto>>(result.Data!, result.Message));
        }

        /// <summary>
        /// Marks notification as read with cache invalidation
        /// </summary>
        [HttpPut("{notificationId:int}/read")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<NotificationMarkAsReadResponseDto>>> MarkNotificationAsRead(
            [FromRoute] int notificationId,
            [FromBody] NotificationMarkAsReadRequestBody requestBody)
        {

            _logger.LogInformation("MarkNotificationAsRead - NotificationId: {NotificationId}, UserId: {UserId}",
                notificationId, requestBody.UserId);

            var request = new NotificationMarkAsReadRequestDto
            {
                NotificationId = notificationId,
                UserId = requestBody.UserId
            };

            var result = await _notificationService.MarkNotificationAsReadAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("MarkNotificationAsRead failed - NotificationId: {NotificationId}: {Message}",
                    notificationId, result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Notification marked as read - NotificationId: {NotificationId}, WasUpdated: {WasUpdated}",
                notificationId, result.Data?.WasUpdated);

            return Ok(new SuccessResponse<NotificationMarkAsReadResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Gets unread notification count with aggressive caching
        /// </summary>
        [HttpGet("{userId:int}/unread-count")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<int>>> GetUnreadNotificationCount(
            [FromRoute] int userId)
        {

            _logger.LogInformation("GetUnreadNotificationCount for UserId: {UserId}", userId);

            var result = await _notificationService.GetUnreadNotificationCountAsync(userId);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetUnreadNotificationCount failed for UserId {UserId}: {Message}", userId, result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Unread count retrieved for UserId {UserId}: {Count}", userId, result.Data);

            return Ok(new SuccessResponse<int>(result.Data!, result.Message));
        }
    }
}