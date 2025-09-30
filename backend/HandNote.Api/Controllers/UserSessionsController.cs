using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Interfaces;
using HandNote.Api.Responses;

namespace HandNote.Api.Controllers
{
    [Route("api/user-sessions")]
    [ApiController]
    public class UserSessionsController : ControllerBase
    {
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly ILogger<UserSessionsController> _logger;

        public UserSessionsController(
            IUserSessionRepository userSessionRepository,
            ILogger<UserSessionsController> logger)
        {
            _userSessionRepository = userSessionRepository;
            _logger = logger;
        }

        /// <summary>
        /// Logout a specific user session
        /// </summary>
        /// <param name="request">Logout details including email and session ID</param>
        /// <returns>Logout result</returns>
        [HttpPost("logout")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<LogoutUserResponseDto>>> LogoutUser(
            [FromBody] LogoutUserRequestDto request)
        {
            _logger.LogInformation("LogoutUser request for Email: {Email}, SessionId: {SessionId}",
                request.Email, request.SessionId);

            var result = await _userSessionRepository.LogoutUserAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("LogoutUser failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("User logged out successfully - Email: {Email}", request.Email);

            return Ok(new SuccessResponse<LogoutUserResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Logout all sessions for a user
        /// </summary>
        /// <param name="request">Logout details including email</param>
        /// <returns>Logout all sessions result</returns>
        [HttpPost("logout-all")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<LogoutAllSessionsResponseDto>>> LogoutAllSessions(
            [FromBody] LogoutAllSessionsRequestDto request)
        {
            _logger.LogInformation("LogoutAllSessions request for Email: {Email}", request.Email);

            var result = await _userSessionRepository.LogoutAllSessionsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("LogoutAllSessions failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("All sessions logged out successfully - Email: {Email}, Count: {Count}",
                request.Email, result.Data?.SessionsLoggedOut ?? 0);

            return Ok(new SuccessResponse<LogoutAllSessionsResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Get all active sessions for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>List of active sessions</returns>
        [HttpGet("{userId}/active")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<GetActiveSessionsResponseDto>>> GetActiveSessions(
            [FromRoute] int userId)
        {
            _logger.LogInformation("GetActiveSessions request for UserId: {UserId}", userId);

            var request = new GetActiveSessionsRequestDto { UserId = userId };
            var result = await _userSessionRepository.GetActiveSessionsAsync(request);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("GetActiveSessions failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Active sessions retrieved successfully - UserId: {UserId}, Count: {Count}",
                userId, result.Data?.Sessions?.Count ?? 0);

            return Ok(new SuccessResponse<GetActiveSessionsResponseDto>(result.Data!, result.Message));
        }
    }
}