using Microsoft.AspNetCore.Mvc;
using PlayerHub.Models;
using System.Text.Json;
using vibetogether_backend.Context;
using vibetogether_backend.DTO;
using vibetogether_backend.Models;

namespace vibetogether_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private MainContext _context;
        public RoomController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id) { 
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
            return Json(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreate model)
        {
            string? userInfo = User?.Claims?.FirstOrDefault(x => x.Type == "UserInfo")?.Value;
            Guid? userId = null;
            if (userInfo != null)
            {
                var user = JsonSerializer.Deserialize<User>(userInfo);
                if (user != null)
                {
                    userId = user.UserId;
                }
            }

            var room = new Rooms { Id = Guid.NewGuid(), VideoURL = model.VideoUrl, UserId = userId };
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();

            return Json(room);
        }
    }
}
