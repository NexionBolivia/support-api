using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SupportAPI.Services.Services;
using Newtonsoft.Json.Linq;
using Support.API.Services.Data;
using Support.API.Services.Models;

namespace support_api.Controllers
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
        public LoginResponse Authenticate(JObject? data)
        {
            return this.profileService.Login(data);
        }
    }
}
