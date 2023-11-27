using Microsoft.AspNetCore.Identity;
using System.Text;
using VibeTogether.Authorization.JWT;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.Services
{
    public interface IAuthorizationService
    {
        Task<string> Login(LoginModel lm);

        Task<string> Register(RegisterModel lm);

        void Logout();
    }
}
