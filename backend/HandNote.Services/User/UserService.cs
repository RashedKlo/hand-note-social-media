using System;
using System.Threading.Tasks;
using HandNote.Data.DTOs.User;
using HandNote.Data.Interfaces;
using HandNote.Data.Models;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Services.Interfaces;
using Microsoft.Extensions.Logging;
using HandNote.Data.Results;
using HandNote.Services.User;

namespace HandNote.Services.User
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<UserAuthenticationData>> RegisterUserAsync(UserRegistrationDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Registration attempted with null data");
                return OperationResult<UserAuthenticationData>.Failure("Registration data is required");
            }

            _logger.LogInformation("Processing user registration for Password: {Password}, email: {Email}",
                dto.Password, dto.Email);

            // Validate registration data
            var validationResult = await UserValidation.ValidateRegistrationAsync(dto, _userRepository, _logger);
            if (!validationResult.IsSuccess)
            {
                _logger.LogWarning("Registration validation failed for {Username}: {Error}",
                    dto.UserName, validationResult.Message);
                return OperationResult<UserAuthenticationData>.Failure(validationResult.Message);
            }

            // Create user via repository
            var createResult = await _userRepository.CreateUserAsync(dto);

            if (!createResult.IsSuccess)
            {
                _logger.LogWarning("User registration failed for {Username}: {Message}",
                    dto.UserName, createResult.Message);
                return OperationResult<UserAuthenticationData>.Failure(createResult.Message);
            }

            _logger.LogInformation("User registered successfully - UserId: {UserId}",
                createResult.Data?.User?.UserId);

            return OperationResult<UserAuthenticationData>.Success(createResult.Data!, createResult.Message);
        }

        public async Task<OperationResult<UserAuthenticationData>> LoginUserAsync(UserLoginDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Login attempted with null data");
                return OperationResult<UserAuthenticationData>.Failure("Login credentials are required");
            }

            _logger.LogInformation("Processing user login for email: {Email}", dto.Email);

            var authResult = await _userRepository.LoginUserAsync(dto);

            if (!authResult.IsSuccess)
            {
                _logger.LogWarning("Login failed for email: {Email}: {Message}", dto.Email, authResult.Message);
                return OperationResult<UserAuthenticationData>.Failure(authResult.Message);
            }

            _logger.LogInformation("User logged in successfully - UserId: {UserId}",
                authResult.Data?.User?.UserId);

            return OperationResult<UserAuthenticationData>.Success(authResult.Data!, authResult.Message);
        }


    }


}