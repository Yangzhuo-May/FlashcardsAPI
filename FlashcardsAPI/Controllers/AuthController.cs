using FlashcardsAPI.Dtos;
using FlashcardsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;
        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
        {
            try
            {
                _userService.AddUser(registerRequest);
                return Ok(new { message = "User added successfully" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var response = _userService.CheckUser(loginRequest);
                return Ok(new { token = response.Token });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
