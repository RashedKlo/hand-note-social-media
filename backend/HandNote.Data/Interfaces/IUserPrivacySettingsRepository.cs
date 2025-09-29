using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Results;

namespace HandNote.Data.Interfaces
{
    public interface IUserPrivacySettingsRepository
    {
        Task<OperationResult<UserPrivacySettingsResetResponseDto>> ResetUserPrivacySettingsAsync (int UserID );
        Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> UpdateUserPrivacySettingsAsync(UserPrivacySettingsUpdateRequestDto dto);
    }
}