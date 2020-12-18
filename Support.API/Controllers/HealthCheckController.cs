using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public HealthCheckController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("healthcheck")]
        public IActionResult All()
        {
            return Ok("ok");
        }

        [Route("healthcheck/db")]
        public async Task<IActionResult> HealthCheckDB()
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();
            if(canConnect)
                return Ok("ok");

            return BadRequest("DB not accessible");
        }

        [Route("seed")]
        public IActionResult Seed()
        {
            _dbContext.SeedData(false);

            return Ok("ok");
        }
    }
}
