using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VibeTogether.Authorization.Models;
using VibeTogether.Authorization.Services;

namespace vibetogether_backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthorizationService _as;
        private string _token;

        public AccountController(SignInManager<VibeUser> signInManager, UserManager<VibeUser> userManager)
        {
            _as = new AuthorizationService(signInManager, userManager);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _token = await _as.Login(loginData);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Json(_token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _token = await _as.Register(registerData);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Json(_token);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _as.Logout();
            return View();
        }
    }
}
