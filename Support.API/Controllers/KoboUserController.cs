using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Support.API.Services.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Support.API.Services.KoboData;

namespace Support.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KoboUserController : ControllerBase
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly KoboDbContext koboDbContext;
        private readonly IKoboUserService koboUserService;


        public KoboUserController(ApplicationDbContext appContext, KoboDbContext koboContext)
        {
            this.applicationDbContext = appContext;
            this.koboDbContext = koboContext;
            this.koboUserService = new KoboUserService(appContext, koboContext);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var response = this.koboUserService.GetAll();
            if (response.Count() > 0)
            {
                return Ok(response);
            }
            else return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}