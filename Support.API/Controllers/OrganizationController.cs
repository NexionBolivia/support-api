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

        private readonly ApplicationDbContext applicationDbContext;
        private readonly IOrganizationService organizationService;


        public OrganizationController(ApplicationDbContext appContext)
        {
            this.applicationDbContext = appContext;
            this.organizationService = new OrganizationService(appContext);
        }

        [HttpGet]
        [ActionName("All")]
        public ActionResult GetAll()
        {
            ActionResult actionResult;
            IEnumerable<OrganizationResponse> all = this.organizationService.GetAll();
            if (Enumerable.Count<OrganizationResponse>(all) <= 0)
            {
                actionResult = this.StatusCode(204);
            }
            else
            {
                actionResult = this.Ok(all);
            }
            return actionResult;
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
