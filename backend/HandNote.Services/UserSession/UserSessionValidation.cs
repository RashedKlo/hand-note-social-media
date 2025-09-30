using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.UserSession
{
    public static class UserSessionValidation
    {
        public static Task<OperationResult<bool>> ValidateLogoutAsync(
            LogoutUserRequestDto dto,
            ILogger logger)
        {
            // Validate email
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email is required and cannot be empty"));
            }

            if (dto.Email.Length > 320)
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email must not exceed 320 characters"));
            }

            // Basic email format validation (more detailed validation is in DataAnnotations)
            if (!dto.Email.Contains("@"))
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email must be in a valid format"));
            }

            // Validate session ID
            if (dto.SessionId <= 0)
            {
                return Task.FromResult(OperationResult<bool>.Failure("SessionId must be a positive integer"));
            }

            logger.LogDebug("Logout validation passed for Email: {Email}, SessionId: {SessionId}",
                dto.Email, dto.SessionId);

            return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
        }

        public static Task<OperationResult<bool>> ValidateLogoutAllSessionsAsync(
            LogoutAllSessionsRequestDto dto,
            ILogger logger)
        {
            // Validate email
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email is required and cannot be empty"));
            }

            if (dto.Email.Length > 320)
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email must not exceed 320 characters"));
            }

            // Basic email format validation
            if (!dto.Email.Contains("@"))
            {
                return Task.FromResult(OperationResult<bool>.Failure("Email must be in a valid format"));
            }

            logger.LogDebug("Logout all sessions validation passed for Email: {Email}", dto.Email);

            return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
        }

        public static Task<OperationResult<bool>> ValidateGetActiveSessionsAsync(
            GetActiveSessionsRequestDto dto,
            ILogger logger)
        {
            // Validate user ID
            if (dto.UserId <= 0)
            {
                return Task.FromResult(OperationResult<bool>.Failure("UserId must be a positive integer"));
            }

            logger.LogDebug("Get active sessions validation passed for UserId: {UserId}", dto.UserId);

            return Task.FromResult(OperationResult<bool>.Success(true, "Validation passed"));
        }
    }
}