using Microsoft.AspNetCore.Mvc;

namespace vibetogether_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {

        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Index")]
        public IActionResult Index()
        {

            return Ok("Healthy");
        }
    }
}