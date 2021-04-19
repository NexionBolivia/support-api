using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DummyController> _logger;
        /// <summary>
        ///     Called by DI
        /// </summary>
        /// <param name="logger"></param>
        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Get weatherForecast dummy
        /// </summary>
        /// <response code="200">Returns list of forecast</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
