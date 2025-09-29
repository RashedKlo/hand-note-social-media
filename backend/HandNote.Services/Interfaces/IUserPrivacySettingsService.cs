using System.Threading.Tasks;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IUserPrivacySettingsService
    {
       Task<OperationResult<UserPrivacySettingsUpdateResponseDto>> UpdateUserPrivacySettingsAsync(UserPrivacySettingsUpdateRequestDto dto);
        Task<OperationResult<UserPrivacySettingsResetResponseDto>> ResetUserPrivacySettingsAsync(int UserId);
    }
}
