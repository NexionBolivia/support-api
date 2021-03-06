using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support.API.Services.Data;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System.Collections.Generic;
using System.Linq;

namespace Support.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IProfileService profileService;


        public ProfileController(ApplicationDbContext context)
        {
            this.context = context;
            this.profileService = new ProfileService(context);
        }

        [HttpGet]
        public IEnumerable<ProfileRequest> GetProfiles(string userName)
        {
            var response = this.profileService.GetProfiles(userName);
            if (response.Count() > 0)
            {
                return response;
            }
            else return null; 
        }

        [HttpPost]
        public ActionResult CreateProfile(ProfileRequest data)
        {
            if (this.profileService.CreateProfile(data)) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public ActionResult UpdateProfile(string username, ProfileRequest data)
        {
            if (this.profileService.UpdateProfile(data)) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
