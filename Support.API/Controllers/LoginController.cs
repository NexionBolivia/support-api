using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Support.API.Services.Services;
using Newtonsoft.Json.Linq;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Microsoft.AspNetCore.Http;

namespace Support.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IProfileService profileService;


        public LoginController(ApplicationDbContext context)
        {
            this.context = context;
            this.profileService = new ProfileService(context);
        }

        [HttpPost]
        public ActionResult<LoginResponse> Authenticate(LoginRequest data)
        {
            if (this.profileService.Login(data).Token != null)
            {
                return Ok(this.profileService.Login(data));
            }
            else return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
