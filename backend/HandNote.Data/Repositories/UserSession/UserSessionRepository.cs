using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Logout;
using HandNote.Data.DTOs.UserSession.Query;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.UserSession.Commands;
using HandNote.Data.Repositories.UserSession.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Data.Repositories.UserSession
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ILogger<UserSessionRepository> _logger;

        public UserSessionRepository(ILogger<UserSessionRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<LogoutUserResponseDto>> LogoutUserAsync(LogoutUserRequestDto dto)
        {
            _logger.LogInformation("Repository: Logging out user - Email: {Email}", dto.Email);
            return await LogoutUserCommand.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<LogoutAllSessionsResponseDto>> LogoutAllSessionsAsync(LogoutAllSessionsRequestDto dto)
        {
            _logger.LogInformation("Repository: Logging out all sessions - Email: {Email}", dto.Email);
            return await LogoutAllSessionsCommand.ExecuteAsync(dto, _logger);
        }

        public async Task<OperationResult<GetActiveSessionsResponseDto>> GetActiveSessionsAsync(GetActiveSessionsRequestDto dto)
        {
            _logger.LogInformation("Repository: Getting active sessions - UserId: {UserId}", dto.UserId);
            return await GetActiveSessionsQuery.ExecuteAsync(dto, _logger);
        }
    }
}
