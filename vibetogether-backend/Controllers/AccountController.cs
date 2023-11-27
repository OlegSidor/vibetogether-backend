using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VibeTogether.Authorization.JWT;
using VibeTogether.Authorization.Models;
using VibeTogether.Authorization.Services;

namespace vibetogether_backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizationService _as;
        public AccountController(IAuthorizationService authorizationService)
        {
            _as = authorizationService;
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
                return Json(await _as.Login(loginData));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
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
                return Json(await _as.Register(registerData));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _as.Logout();
            return View();
        }
    }
}
