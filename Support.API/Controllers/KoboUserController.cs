using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Extensions;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Collections.Generic;
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

        /// <summary>
        ///     List of users detail
        /// </summary>
        /// <response code="200"></response>
        [ProducesResponseType(typeof(IEnumerable<KoboUserDetail>), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var response = this.koboUserService.GetAll();
            if (response.Count() > 0)
            {
                return Ok(response);
            }
            else return NoContent();
        }

        /// <summary>
        ///     Updates roles and organizations for a user
        /// </summary>
        /// <response code="200"></response>
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        ///     Get UserId, Assets and organizations associated
        /// </summary>
        /// <response code="200">Annonymous type:  { koboUserId = int, assets = [], organizations = [] }</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("UserResources")]
        public async Task<IActionResult> GetUserResources()
        {
            var userName = httpContextAccessor.HttpContext.GetCurrentUserName(); // For loggedIn user only
            var koboUserId = await koboUserService.GetKoboUserIdForKoboUsername(userName);
            var koboUserRoles = koboUserService.GetRolesByKoboUsername(userName);
            var assetsForUser = await koboUserService.GetAssetsForCurrentUser(userName);
            var organizationsForUser = koboUserService.GetOrganizationsByKoboUsername(userName);
            return Ok(
                new {
                    koboUserId = koboUserId,
                    roles = koboUserRoles,
                    assets = assetsForUser,
                    organizations = organizationsForUser
                });
        }

        [HttpPost]
        [Route("activate-user")]
        public async Task<ActionResult> ActivateUser([FromBody] ActivateUserRequest request)
        {
            var result = await koboUserService.ActivateUser(request.UserName);
            return Ok(result);
        }
    }
}