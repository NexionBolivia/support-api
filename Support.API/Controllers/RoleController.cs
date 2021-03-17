using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Services;
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

        [HttpGet]
        [ActionName("All")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await this.roleService.GetAll());
        }
    }
}