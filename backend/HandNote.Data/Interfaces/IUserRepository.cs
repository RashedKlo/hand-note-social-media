
using HandNote.Data.Results;
using HandNote.Data.DTOs.User;
using HandNote.Data.Repositories.User.Helpers;

namespace HandNote.Data.Interfaces
{
    public interface IUserRepository
    {

        Task<OperationResult<UserAuthenticationData>> CreateUserAsync(UserRegistrationDto dto);
        Task<OperationResult<UserAuthenticationData>> LoginUserAsync(UserLoginDto dto);
        Task<OperationResult<Models.User>> GetUserByEmailAsync(string Email);
    }
}