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
    [Route("[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        /// <summary>
        ///     Get all profiles
        /// </summary>
        /// <response code="200">List of profiles</response>
        [ProducesResponseType(typeof(IEnumerable<ProfileRequest>), StatusCodes.Status200OK)]
        [HttpGet]
        [ActionName("All")]
        public ActionResult<IEnumerable<ProfileRequest>> GetProfiles()
        {
            return Ok(this.profileService.GetProfiles());
        }

        /// <summary>
        ///     Get a profile by Id
        /// </summary>
        /// <response code="200">Instance of ProfileRequest</response>
        [ProducesResponseType(typeof(ProfileRequest), StatusCodes.Status200OK)]
        [HttpGet]
        [ActionName("Get")]
        public ActionResult<ProfileRequest> GetProfile(int profileId)
        {
            var response = this.profileService.GetProfile(profileId);
            if(string.IsNullOrEmpty(response.ProfileId)) return StatusCode(StatusCodes.Status204NoContent);
            else return Ok(this.profileService.GetProfile(profileId));
        }

        /// <summary>
        ///     Saves a profile. If it doesn't exist, it creates, if it exists there's an update
        /// </summary>
        /// <response code="200">Annonymous object { profileId: string }</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [ActionName("Update")]
        public ActionResult CreateUpdateProfile(ProfileRequest data)
        {
            var response = this.profileService.CreateUpdateProfile(data);
            if (!string.IsNullOrEmpty(response)) return Ok(new
            {
                profileId = response
            });
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
