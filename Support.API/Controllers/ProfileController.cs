﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ApplicationDbContext context;
        private readonly IProfileService profileService;


        public ProfileController(ApplicationDbContext context)
        {
            this.context = context;
            this.profileService = new ProfileService(context);
        }

        [HttpGet]
        [ActionName("All")]
        public ActionResult<IEnumerable<ProfileRequest>> GetProfiles()
        {
            return Ok(this.profileService.GetProfiles());
        }

        [HttpGet]
        [ActionName("Get")]
        public ActionResult<ProfileRequest> GetProfile(int profileId)
        {
            var response = this.profileService.GetProfile(profileId);
            if(string.IsNullOrEmpty(response.ProfileId)) return StatusCode(StatusCodes.Status204NoContent);
            else return Ok(this.profileService.GetProfile(profileId));
        }

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
