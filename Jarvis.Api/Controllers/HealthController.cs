using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jarvis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {
                status = "healthy",
                service = "Jarvis.Api",
                version = "0.1.0",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
