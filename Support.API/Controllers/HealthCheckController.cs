using Microsoft.AspNetCore.Mvc;

namespace Support.Api.Controllers
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
