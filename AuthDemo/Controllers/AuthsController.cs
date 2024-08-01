using AuthDemo.Models.Entities;
using AuthDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignIn signIn)
        {
            var response = await _authService.RegisterUser(signIn);
            if (response != null)
            {
                return Ok(new { Result = "Registration successful" });
            }
            return BadRequest("Registration failed");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            var response = await _authService.LoginUser(login);
            if (response != null)
            {
                return Ok(response);
            }
            return Unauthorized("Invalid login attempt");
        }
    }
}
