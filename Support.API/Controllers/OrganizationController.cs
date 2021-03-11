using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Collections.Generic;
using System.Linq;

namespace Support.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet]
        [ActionName("All")]
        public ActionResult GetAll()
        {
            return Ok(this.organizationService.GetAll());
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(string organizationId)
        {
            if (this.organizationService.DeleteOrganization(organizationId))
            {
                return Ok();
            }
            else return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult CreateUpdate(OrganizationRequest data)
        {
            var response = this.organizationService.CreateUpdateOrganization(data);
            if (!string.IsNullOrEmpty(response)) return Ok(new
            {
                organizationId = response
            });
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
