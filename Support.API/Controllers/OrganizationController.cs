using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Collections.Generic;

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

        /// <summary>
        ///     Get all organizations
        /// </summary>
        /// <response code="200">List of organizations</response>
        [ProducesResponseType(typeof(IEnumerable<OrganizationResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        [ActionName("All")]
        public ActionResult GetAll()
        {
            return Ok(this.organizationService.GetAll());
        }

        /// <summary>
        ///     Deletes an organization and its relations with children and profiles
        /// </summary>
        /// <response code="200">Organization was deleted</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        ///     Saves an organization. If it doesn't exist, it creates, if it exists there's an update
        /// </summary>
        /// <response code="200">Annonymous object: { organizationId: string }</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
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
