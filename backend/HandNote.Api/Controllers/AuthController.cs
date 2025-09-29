using Microsoft.AspNetCore.Mvc;
using HandNote.Data.DTOs.User;
using HandNote.Services.Interfaces;
using HandNote.Data.Results;
using HandNote.Api.Responses;
using HandNote.Data.Repositories.User.Helpers;
using Microsoft.IdentityModel.Tokens;
using HandNote.Data.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.Interfaces;

namespace HandNote.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
            {
                _logger.LogWarning("Registeration failed: {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }
            _logger.LogInformation("User created successfully - ID: {UserId}", result.Data?.User?.UserId);

            return Ok(new SuccessResponse<UserAuthenticationData>(result?.Data!, result?.Message!));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

            var result = await _userService.LoginUserAsync(dto);
            if (!result.IsSuccess)
            {
                _logger.LogInformation("Login failed : {Message}", result.Message);
                return BadRequest(new ErrorResponse(result.Message, result.Errors.ToList()));
            }
            _logger.LogInformation("User Login successfuly - Email :{Email}", dto.Email);
            return Ok(new SuccessResponse<UserAuthenticationData>(result?.Data!, result?.Message!));

        }



        // [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        /// <summary>
        /// Initiates Google OAuth login
        /// </summary>
        [HttpGet("sign-google")]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                // RedirectUri = Url.Action(nameof(GoogleCallback), "Auth")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Handles Google OAuth callback and redirects to frontend with consistent error handling
        /// </summary>
        [HttpGet("callback-google")]
        public async Task<IActionResult> GoogleCallback()
        {
            string _frontendUrl = "http://localhost:5173";

            try
            {
                var authResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!authResult.Succeeded)
                {
                    _logger.LogWarning("Google OAuth authentication failed");
                    return Redirect($"{_frontendUrl}/googlelogin?error=authentication_failed");
                }

                // Extract user info from claims
                var Email = authResult.Principal.FindFirst(c => c.Type == "email")?.Value;

                if (string.IsNullOrEmpty(Email))
                {
                    _logger.LogError("User information not found Email : {Email}", Email);
                    return Redirect($"{_frontendUrl}/googlelogin?error=user_info_not_found");
                }

                _logger.LogInformation("Google OAuth attempt for email: {Email}", Email);

                // Register or login user and get authentication data
                var result = await _userService.AuthenticateWithGoogleAsync(Email);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Google authentication failed: {Message}", result.Message);
                    return Redirect($"{_frontendUrl}/googlelogin?error=authentication_failed&message={Uri.EscapeDataString(result.Message)}");
                }

                _logger.LogInformation("Google OAuth successful for email: {Email}", Email);

                // Redirect back to frontend with token on success
                return Redirect($"{_frontendUrl}/googlelogin?token={result.Data?.AccessToken}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Google OAuth callback");
                return Redirect($"{_frontendUrl}/googlelogin?error=internal_error&message={Uri.EscapeDataString("An error occurred during Google authentication")}");
            }
        }
    }
}