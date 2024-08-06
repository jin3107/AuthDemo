using AuthDemo.Models.Entities;
using AuthDemo.Models.Response;
using AuthDemo.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthDemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterUser(SignIn signIn)
        {
            var result = await _authRepository.RegisterUser(signIn);
            if (result.Succeeded)
            {
                return new AuthResponse
                {
                    AccessToken = null,
                    ExpiresIn = DateTime.UtcNow
                };
            }
            return null;
        }

        public async Task<AuthResponse> LoginUser(Login login)
        {
            var result = await _authRepository.LoginUser(login);
            if (result.Succeeded)
            {
                var user = await _authRepository.FindUserByName(login.UserName!);
                if (user == null)
                {
                    return null;
                }

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new AuthResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresIn = token.ValidTo
                };
            }
            return null; 
        }
    }
}
