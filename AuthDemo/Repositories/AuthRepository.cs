using AuthDemo.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(SignIn signIn)
        {
            var user = new IdentityUser
            {
                UserName = signIn.UserName,
                Email = signIn.Email
            };

            return await _userManager.CreateAsync(user, signIn.Password!);
        }

        public async Task<SignInResult> LoginUser(Login login)
        {
            return await _signInManager.PasswordSignInAsync(login.UserName!, login.Password!, false, false);
        }

        public async Task<IdentityUser> FindUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
