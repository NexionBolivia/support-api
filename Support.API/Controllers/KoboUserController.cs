using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Extensions;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class KoboUserController : ControllerBase
    {
        private readonly IKoboUserService koboUserService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public KoboUserController(IKoboUserService koboUserService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.koboUserService = koboUserService;
            this.httpContextAccessor = httpContextAccessor;
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

        [HttpPost]
        public ActionResult UpdateKoboUser(KoboUserRequest data)
        {
            ActionResult actionResult;
            if (!this.koboUserService.UpdateKoboUser(data))
            {
                actionResult = this.StatusCode(500);
            }
            else
            {
                actionResult = this.Ok();
            }
            return actionResult;
        }

        [HttpGet]
        [Route("UserResources")]
        public async Task<IActionResult> GetUserResources()
        {
            var userName = httpContextAccessor.HttpContext.GetCurrentUserName(); // For loggedIn user only
            var koboUserId = await koboUserService.GetKoboUserIdForKoboUsername(userName);
            var assetsForUser = await koboUserService.GetAssetsForCurrentUser(userName);
            var organizationsForUser = koboUserService.GetOrganizationsByKoboUsername(userName);
            return Ok(
                new {
                    koboUserId = koboUserId,
                    assets = assetsForUser,
                    organizations = organizationsForUser
                });
        }
    }
}