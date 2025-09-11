using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.User;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.User.Commands;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Repositories.User.Queries;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;
namespace HandNote.Data.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<OperationResult<UserAuthenticationData>> CreateUserAsync(UserRegistrationDto user)
        {
            return await CreateUserCommand.ExecuteAsync(user, _logger);
        }
        public async Task<OperationResult<UserAuthenticationData>> LoginUserAsync(UserLoginDto user)
        {
            return await LoginUserCommand.ExecuteAsync(user, _logger);
        }

        public async Task<OperationResult<Models.User>> GetUserByEmailAsync(string Email)
        {
            return await GetUserByEmailQuery.ExecuteAsync(Email, _logger);
        }
    }
}