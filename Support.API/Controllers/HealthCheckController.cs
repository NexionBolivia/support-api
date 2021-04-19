using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using Support.API.Services.KoboFormData;
using Support.API.Services.Models.Request;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    /// <summary>
    /// Health check controller
    /// </summary>
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly KoboFormDbContext _koboDbContext;
        private readonly ILogger<HealthCheckController> logger;

        /// <summary>
        /// Init controller
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="koboDbContext"></param>
        public HealthCheckController(ApplicationDbContext dbContext,
                                     KoboFormDbContext koboDbContext,
                                     ILogger<HealthCheckController> logger)
        {
            _dbContext = dbContext;
            _koboDbContext = koboDbContext;
            this.logger = logger;
        }

        /// <summary>
        ///     Check if api server is reachable
        /// </summary>
        /// <response code="200">API is accessible</response>
        [Route("healthcheck")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult All()
        {
            return Ok("ok");
        }

        /// <summary>
        ///     Check if Support API db (Postgresql) is reacheable
        /// </summary>
        /// <response code="200">Support API DB is accessible</response>
        [Route("healthcheck/db")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HealthCheckDB()
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();
            if(canConnect)
                return Ok("ok");

            return BadRequest("Support DB not accessible");
        }

        /// <summary>
        ///     Returns healthcheck against KoboForms db
        /// </summary>
        /// <response code="200">Koboform is reacheable</response>
        [Route("healthcheck/kobo-db-conn")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        ///     Performs Seed of DATA and STRUCTURE creation
        /// </summary>
        /// <response code="200">Data Seed performed</response>
        [Route("seed")]
        [HttpPut] [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Seed(SeedRequest request)
        {
            _dbContext.SeedData(logger, request);

            return Ok("ok");
        }

        /// <summary>
        ///     Checks if an token call (using Authorize 'Bearer [token]') is accepted
        /// </summary>
        /// <response code="200">Call can be performed, token is valid</response>
        [Route("check-token")]
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AutorizedCall()
        {
            return Ok("ok");
        }
    }
}
