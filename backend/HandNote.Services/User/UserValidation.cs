using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.User;
using HandNote.Data.Interfaces;
using HandNote.Data.Repositories.User;
using HandNote.Data.Results;
using Microsoft.Extensions.Logging;

namespace HandNote.Services.User
{
    public class UserValidation
    {
        public static async Task<OperationResult<bool>> ValidateRegistrationAsync(UserRegistrationDto dto, IUserRepository userRepository, ILogger<UserService> logger)
        {
            if (dto == null)
                return OperationResult<bool>.Failure("Registration data cannot be null");
            var existingUser = await userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                // Check for conflicts - prioritize email conflict as it's typically the unique identifier
                if (string.Equals(existingUser?.Data?.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogError("Email is already registered");
                    return OperationResult<bool>.Failure("Email is already registered");
                }
                if (string.Equals(existingUser?.Data?.Email, dto.UserName, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogError("Username is already registered");
                    return OperationResult<bool>.Failure("Username is already taken");
                }
            }
            return OperationResult<bool>.Success(true);
        }



    }
}