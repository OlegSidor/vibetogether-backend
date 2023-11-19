using Microsoft.AspNetCore.Identity;
using System.Text;
using VibeTogether.Authorization.Helpers;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.Services
{
    public class AuthorizationService
    {
        private readonly SignInManager<VibeUser> _signInManager;
        private readonly UserManager<VibeUser> _userManager;

        public AuthorizationService(SignInManager<VibeUser> sim, UserManager<VibeUser> um)
        {
            _signInManager = sim;
            _userManager = um;
        }

        public async Task<string> Login(LoginModel lm)
        {
            var loginResult = _signInManager.PasswordSignInAsync(lm.Username, lm.Password, lm.RememberMe, false).Result;

            if (loginResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(lm.Username);
                return JwtHelper.GenerateJwtToken(user);
            }
            else
            {
                throw new InvalidOperationException("Invalid data");
            }
        }

        public async Task<string> Register(RegisterModel lm)
        {
            VibeUser user = new()
            {
                UserName = lm.Username,
                Email = lm.Email,
            };

            var registerResult = _userManager.CreateAsync(user, lm.Password).Result;

            if (registerResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return JwtHelper.GenerateJwtToken(user);
            }
            else
            {
                StringBuilder sb = new();
                foreach (var error in registerResult.Errors)
                {
                    sb.Append(error.Description);
                }

                throw new InvalidOperationException(sb.ToString());
            }
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}
