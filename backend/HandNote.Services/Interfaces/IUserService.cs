using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.User;
using HandNote.Data.Models;
using HandNote.Data.Repositories.User.Helpers;
using HandNote.Data.Results;

namespace HandNote.Services.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<UserAuthenticationData>> RegisterUserAsync(UserRegistrationDto dto);
        Task<OperationResult<UserAuthenticationData>> LoginUserAsync(UserLoginDto dto);
    }
}