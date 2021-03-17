using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Models;
using Support.API.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        /// <summary>
        ///     Get all roles
        /// </summary>
        /// <response code="200">List of roles</response>
        [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        [ActionName("All")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await this.roleService.GetAll());
        }
    }
}