using System;
using System.Threading.Tasks;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.DTOs.UserPrivacySettings.Update;

namespace HandNote.Services.UserPrivacySettings
{
    public sealed class UserPrivacySettingsService : IUserPrivacySettingsService
    {
        private readonly IUserPrivacySettingsRepository _privacySettingsRepository;
        private readonly ILogger<UserPrivacySettingsService> _logger;

        public UserPrivacySettingsService(
            IUserPrivacySettingsRepository privacySettingsRepository,
            ILogger<UserPrivacySettingsService> logger)
        {
            _privacySettingsRepository = privacySettingsRepository ?? throw new ArgumentNullException(nameof(privacySettingsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<UserPrivacySettingsResetResponseDto>> ResetUserPrivacySettingsAsync(int UserId)
        {

            _logger.LogInformation("Processing privacy settings reset for UserId: {UserId}", UserId);

            var validationResult = await UserPrivacySettingsValidation.ValidateResetAsync(UserId, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Privacy settings reset validation failed for UserId {UserId}: {Error}",
                    UserId, validationResult.Message);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure(validationResult.Message);
            }

            var resetResult = await _privacySettingsRepository.ResetUserPrivacySettingsAsync(UserId);

            if (!resetResult.IsSuccess)
            {
                _logger.LogWarning("Privacy settings reset failed for UserId {UserId}: {Message}",
                    UserId, resetResult.Message);
                return OperationResult<UserPrivacySettingsResetResponseDto>.Failure(resetResult.Message);
            }

            _logger.LogInformation("Privacy settings reset successfully - UserId: {UserId}", UserId);

            return OperationResult<UserPrivacySettingsResetResponseDto>.Success(resetResult.Data!, resetResult.Message);
        }
   
     public async Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> UpdateUserPrivacySettingsAsync(UserPrivacySettingsUpdateRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Privacy settings update attempted with null data");
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure("Request data is required");
            }

            _logger.LogInformation("Processing privacy settings update for UserId: {UserId}", dto.UserId);

            var validationResult = await UserPrivacySettingsValidation.ValidateUpdateAsync(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Privacy settings update validation failed for UserId {UserId}: {Error}",
                    dto.UserId, validationResult.Message);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure(validationResult.Message);
            }

            var updateResult = await _privacySettingsRepository.UpdateUserPrivacySettingsAsync(dto);

            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning("Privacy settings update failed for UserId {UserId}: {Message}",
                    dto.UserId, updateResult.Message);
                return OperationResult<UserPrivacySettingsUpdateResponseDto>.Failure(updateResult.Message);
            }

            _logger.LogInformation("Privacy settings updated successfully - UserId: {UserId}", dto.UserId);

            return OperationResult<UserPrivacySettingsUpdateResponseDto>.Success(updateResult.Data!, updateResult.Message);
        }
    }
}