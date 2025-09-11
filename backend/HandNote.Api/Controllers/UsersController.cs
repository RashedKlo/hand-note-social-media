using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.User;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.Repositories.User.Helpers;
using Microsoft.IdentityModel.Tokens;
using HandNote.Data.Helpers;

namespace HandNote.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
        {
            _logger.LogInformation("Registration attempt for username: {Username}", dto.UserName);

            var result = await _userService.RegisterUserAsync(dto);

            // Clean, readable result handling
            if (!result.IsSuccess)
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            return Ok(new SuccessResponse<UserAuthenticationData>(result?.Data!, result?.Message!));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

            var result = await _userService.LoginUserAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));

            return Ok(new SuccessResponse<UserAuthenticationData>(result?.Data!, result?.Message!));

        }

    }
}