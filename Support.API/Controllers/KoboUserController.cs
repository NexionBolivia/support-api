using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.KoboData;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Linq;

namespace Support.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class KoboUserController : ControllerBase
    {
        private readonly IKoboUserService koboUserService;

        public KoboUserController(IKoboUserService koboUserService)
        {
            this.koboUserService = koboUserService;
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
    }
}