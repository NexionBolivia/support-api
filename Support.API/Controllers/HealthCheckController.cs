﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using Support.API.Services.KoboData;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly KoboDbContext _koboDbContext;

        public HealthCheckController(ApplicationDbContext dbContext,
                                     KoboDbContext koboDbContext)
        {
            _dbContext = dbContext;
            _koboDbContext = koboDbContext;
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
        ///     Performs Seed of DATA
        /// </summary>
        /// <response code="200">Data Seed performed</response>
        [Route("seed")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Seed()
        {
            _dbContext.SeedData(false);

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
