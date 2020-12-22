using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SupportAPI.Services.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Support.API.Services.Data;
using Support.API.Services.Models;

namespace support_api.Controllers
{
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
        public IEnumerable<CompositeProfile> GetProfiles(JObject? data)
        {
            return this.profileService.GetProfiles(data);
        }

        [HttpPost]
        public ActionResult CreateProfile(JObject? data)
        {
            if (this.profileService.CreateProfile(data)) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public ActionResult UpdateProfile(string username, JObject? data)
        {
            if (this.profileService.UpdateProfile(data)) return Ok();
            else return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
