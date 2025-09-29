using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.UserPrivacySettings.Commands;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.UserPrivacySettings
{
    public class UserPrivacySettingsRepository : IUserPrivacySettingsRepository
    {
        private readonly ILogger<UserPrivacySettingsRepository> _logger;

        public UserPrivacySettingsRepository(ILogger<UserPrivacySettingsRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<UserPrivacySettingsResetResponseDto>> ResetUserPrivacySettingsAsync(int UserId)
        {
            _logger.LogInformation("Repository: Resetting privacy settings for UserId: {UserId}", UserId);
            return await ResetUserPrivacySettingsCommand.ExecuteAsync(UserId, _logger);
        }

        public async Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> UpdateUserPrivacySettingsAsync(UserPrivacySettingsUpdateRequestDto dto)
        {
            _logger.LogInformation("Repository: Updating privacy settings for UserId: {UserId}", dto.UserId);
            return await UpdateUserPrivacySettingsCommand.ExecuteAsync(dto, _logger);
        }
    }
}