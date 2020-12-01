using Microsoft.AspNetCore.Mvc;

namespace support_api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [Route("healthcheck")]
        public IActionResult All()
        {
            return Ok("ok");
        }
    }
}
