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
    [Route("[controller]/[action]")]
    public class OrganizationController : ControllerBase
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly KoboDbContext koboDbContext;
        private readonly IOrganizationService organizationService;


        public OrganizationController(ApplicationDbContext appContext, KoboDbContext koboContext)
        {
            this.applicationDbContext = appContext;
            this.koboDbContext = koboContext;
            this.organizationService = new OrganizationService(appContext, koboContext);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult CreateUpdate(OrganizationRequest data)
        {
            if (this.organizationService.CreateUpdateOrganization(data)) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
