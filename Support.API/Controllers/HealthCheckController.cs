using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using Support.API.Services.KoboFormData;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly KoboFormDbContext _koboDbContext;

        public HealthCheckController(ApplicationDbContext dbContext,
                                     KoboFormDbContext koboDbContext)
        {
            _dbContext = dbContext;
            _koboDbContext = koboDbContext;
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

            return BadRequest("Support DB not accessible");
        }

        [Route("healthcheck/kobo-db-conn")]
        public async Task<IActionResult> HealthCheckKoboDBConnectivity()
        {
            try
            {
                var canConnect = await _koboDbContext.Database.CanConnectAsync();
                if (canConnect)
                    return Ok("ok");
            }
            catch{  }

            return BadRequest("Kobo DB not accessible from Support API!");
        }

        [Route("seed")]
        public IActionResult Seed()
        {
            _dbContext.SeedData(false);

            return Ok("ok");
        }

        [Route("check-token")]
        [Authorize]
        public IActionResult AutorizedCall()
        {
            return Ok("ok");
        }
    }
}
