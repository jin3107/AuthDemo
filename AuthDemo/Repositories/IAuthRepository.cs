using AuthDemo.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(SignIn signIn);
        Task<SignInResult> LoginUser(Login login);
        Task<IdentityUser> FindUserByName(string userName);
    }
}
