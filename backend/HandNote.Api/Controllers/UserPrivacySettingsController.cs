using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.DTOs.UserPrivacySettings.RequestBodies;
using HandNote.Data.Interfaces;
using HandNote.Api.Responses;

namespace HandNote.Api.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class UserPrivacySettingsController : ControllerBase
    {
        private readonly IUserPrivacySettingsRepository _privacySettingsRepository;
        private readonly ILogger<UserPrivacySettingsController> _logger;

        public UserPrivacySettingsController(
            IUserPrivacySettingsRepository privacySettingsRepository,
            ILogger<UserPrivacySettingsController> logger)
        {
            _privacySettingsRepository = privacySettingsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Reset user privacy settings to default values (all public)
        /// </summary>
        /// <param name="userId">The ID of the user whose privacy settings will be reset</param>
        /// <returns>Reset privacy settings</returns>
        [HttpPost("{userId}/reset")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<UserPrivacySettingsResetResponseDto>>> ResetUserPrivacySettings(
            [FromRoute] int userId)
        {
            _logger.LogInformation("ResetUserPrivacySettings request for UserId: {UserId}", userId);

            var result = await _privacySettingsRepository.ResetUserPrivacySettingsAsync(userId);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("ResetUserPrivacySettings failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Privacy settings reset successfully - UserId: {UserId}", userId);

            return Ok(new SuccessResponse<UserPrivacySettingsResetResponseDto>(result.Data!, result.Message));
        }

        /// <summary>
        /// Update user privacy settings
        /// </summary>
        /// <param name="userId">The ID of the user whose privacy settings will be updated</param>
        /// <param name="request">Privacy settings update details</param>
        /// <returns>Updated privacy settings</returns>
        [HttpPut("{userId}/update")]
        [AllowAnonymous] // For testing - remove in production
        public async Task<ActionResult<SuccessResponse<UserPrivacySettingsUpdateResponseDto>>> UpdateUserPrivacySettings(
            [FromRoute] int userId,
            [FromBody] UserPrivacySettingsUpdateRequestBody request)
        {
            _logger.LogInformation("UpdateUserPrivacySettings request for UserId: {UserId}", userId);

            var updateRequest = new UserPrivacySettingsUpdateRequestDto
            {
                UserId = userId,
                DefaultPostAudience = request.DefaultPostAudience,
                ProfileVisibility = request.ProfileVisibility,
                FriendListVisibility = request.FriendListVisibility,
                FriendRequestFrom = request.FriendRequestFrom,
                MessageFrom = request.MessageFrom
            };

            var result = await _privacySettingsRepository.UpdateUserPrivacySettingsAsync(updateRequest);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("UpdateUserPrivacySettings failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }

            _logger.LogInformation("Privacy settings updated successfully - UserId: {UserId}", userId);

            return Ok(new SuccessResponse<UserPrivacySettingsUpdateResponseDto>(result.Data!, result.Message));
        }
    }
}