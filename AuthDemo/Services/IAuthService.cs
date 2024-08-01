using AuthDemo.Models.Entities;
using AuthDemo.Models.Response;

namespace AuthDemo.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUser(SignIn signIn);
        Task<AuthResponse> LoginUser(Login login);

    }
}
