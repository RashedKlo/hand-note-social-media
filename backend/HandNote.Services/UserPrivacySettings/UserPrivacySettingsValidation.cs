using System.Threading.Tasks;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.UserPrivacySettings
{
    public static class UserPrivacySettingsValidation
    {
         public static Task<OperationResult<bool>> ValidateUpdateAsync(
            UserPrivacySettingsUpdateRequestDto dto,
            ILogger logger)
        {
            // Basic validation
            if (dto.UserId <= 0)
            {
                return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));
            }

            // Check if at least one field is provided for update
            if (string.IsNullOrWhiteSpace(dto.DefaultPostAudience) &&
                string.IsNullOrWhiteSpace(dto.ProfileVisibility) &&
                string.IsNullOrWhiteSpace(dto.FriendListVisibility) &&
                string.IsNullOrWhiteSpace(dto.FriendRequestFrom) &&
                string.IsNullOrWhiteSpace(dto.MessageFrom))
            {
                return Task.FromResult(OperationResult<bool>.Failure("At least one privacy setting field must be provided for update"));
            }

            // Validate individual fields if provided
            if (!string.IsNullOrWhiteSpace(dto.DefaultPostAudience) &&
                dto.DefaultPostAudience != "public" && dto.DefaultPostAudience != "friends" && dto.DefaultPostAudience != "only_me")
            {
                return Task.FromResult(OperationResult<bool>.Failure("DefaultPostAudience must be one of: public, friends, only_me"));
            }

            if (!string.IsNullOrWhiteSpace(dto.ProfileVisibility) &&
                dto.ProfileVisibility != "public" && dto.ProfileVisibility != "friends" && dto.ProfileVisibility != "only_me")
            {
                return Task.FromResult(OperationResult<bool>.Failure("ProfileVisibility must be one of: public, friends, only_me"));
            }

            if (!string.IsNullOrWhiteSpace(dto.FriendListVisibility) &&
                dto.FriendListVisibility != "public" && dto.FriendListVisibility != "friends" && dto.FriendListVisibility != "only_me")
            {
                return Task.FromResult(OperationResult<bool>.Failure("FriendListVisibility must be one of: public, friends, only_me"));
            }

            if (!string.IsNullOrWhiteSpace(dto.FriendRequestFrom) &&
                dto.FriendRequestFrom != "public" && dto.FriendRequestFrom != "friends_of_friends")
            {
                return Task.FromResult(OperationResult<bool>.Failure("FriendRequestFrom must be one of: public, friends_of_friends"));
            }

            if (!string.IsNullOrWhiteSpace(dto.MessageFrom) &&
                dto.MessageFrom != "public" && dto.MessageFrom != "friends_of_friends" && dto.MessageFrom != "friends")
            {
                return Task.FromResult(OperationResult<bool>.Failure("MessageFrom must be one of: public, friends_of_friends, friends"));
            }

            logger.LogDebug("Privacy settings update validation passed for UserId: {UserId}", dto.UserId);

            return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
        }
    
      
        public static Task<OperationResult<bool>> ValidateResetAsync(
         int UserId,
            ILogger logger)
        {
            // Basic validation
            if (UserId <= 0)
            {
                return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));
            }

            logger.LogDebug("Privacy settings reset validation passed for UserId: {UserId}", UserId);

            return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
        }
    }
}