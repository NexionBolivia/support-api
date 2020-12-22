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
        public LoginResponse Authenticate(LoginRequest data)
        {
            return this.profileService.Login(data);
        }
    }
}
