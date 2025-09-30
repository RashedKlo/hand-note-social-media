// Update UserSessionService.cs
using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Results;
using HandNote.Services.Interfaces;
using HandNote.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.UserSession
{
    public sealed class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly ILogger<UserSessionService> _logger;

        public UserSessionService(
            IUserSessionRepository userSessionRepository,
            ILogger<UserSessionService> logger)
        {
            _userSessionRepository = userSessionRepository ?? throw new ArgumentNullException(nameof(userSessionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<LogoutUserResponseDto>> LogoutUserAsync(LogoutUserRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Logout attempted with null data");
                return OperationResult<LogoutUserResponseDto>.Failure("Request data is required");
            }

            _logger.LogInformation("Processing logout for Email: {Email}", dto.Email);

            // Validate input
            var validationResult = await UserSessionValidation.ValidateLogoutAsync(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Logout validation failed for Email {Email}: {Error}",
                    dto.Email, validationResult.Message);
                return OperationResult<LogoutUserResponseDto>.Failure(validationResult.Message);
            }

            var result = await _userSessionRepository.LogoutUserAsync(dto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Logout failed for Email {Email}: {Message}",
                    dto.Email, result.Message);
                return OperationResult<LogoutUserResponseDto>.Failure(result.Message);
            }

            _logger.LogInformation("User logged out successfully - Email: {Email}", dto.Email);

            return OperationResult<LogoutUserResponseDto>.Success(result.Data!, result.Message);
        }

        public async Task<OperationResult<LogoutAllSessionsResponseDto>> LogoutAllSessionsAsync(LogoutAllSessionsRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Logout all sessions attempted with null data");
                return OperationResult<LogoutAllSessionsResponseDto>.Failure("Request data is required");
            }

            _logger.LogInformation("Processing logout all sessions for Email: {Email}", dto.Email);

            // Validate input
            var validationResult = await UserSessionValidation.ValidateLogoutAllSessionsAsync(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Logout all sessions validation failed for Email {Email}: {Error}",
                    dto.Email, validationResult.Message);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure(validationResult.Message);
            }

            var result = await _userSessionRepository.LogoutAllSessionsAsync(dto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Logout all sessions failed for Email {Email}: {Message}",
                    dto.Email, result.Message);
                return OperationResult<LogoutAllSessionsResponseDto>.Failure(result.Message);
            }

            _logger.LogInformation("All sessions logged out successfully - Email: {Email}, Count: {Count}",
                dto.Email, result.Data?.SessionsLoggedOut ?? 0);

            return OperationResult<LogoutAllSessionsResponseDto>.Success(result.Data!, result.Message);
        }

        public async Task<OperationResult<GetActiveSessionsResponseDto>> GetActiveSessionsAsync(GetActiveSessionsRequestDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Get active sessions attempted with null data");
                return OperationResult<GetActiveSessionsResponseDto>.Failure("Request data is required");
            }

            _logger.LogInformation("Processing get active sessions for UserId: {UserId}", dto.UserId);

            // Validate input
            var validationResult = await UserSessionValidation.ValidateGetActiveSessionsAsync(dto, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Get active sessions validation failed for UserId {UserId}: {Error}",
                    dto.UserId, validationResult.Message);
                return OperationResult<GetActiveSessionsResponseDto>.Failure(validationResult.Message);
            }

            var result = await _userSessionRepository.GetActiveSessionsAsync(dto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Get active sessions failed for UserId {UserId}: {Message}",
                    dto.UserId, result.Message);
                return OperationResult<GetActiveSessionsResponseDto>.Failure(result.Message);
            }

            _logger.LogInformation("Active sessions retrieved successfully - UserId: {UserId}, Count: {Count}",
                dto.UserId, result.Data?.Sessions?.Count ?? 0);

            return OperationResult<GetActiveSessionsResponseDto>.Success(result.Data!, result.Message);
        }
    }
}