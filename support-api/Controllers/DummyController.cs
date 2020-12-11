using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using support_api.Models;

namespace support_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        private static readonly Dictionary<string, SupportUser> users = new Dictionary<string, SupportUser>(){
          {"ddb11eb9-5ff8-4028-a8c3-ef20cbe576b8",  new SupportUser(){
              id="ddb11eb9-5ff8-4028-a8c3-ef20cbe576b8",
              UserName= "admin",
              Password= "admin",
              Name= "EPSAs",
              Formation= "Formacion",
              Address= "direccion",
              Phone= "+591-00000000",
              Professionals= 10,
              Employes= 20,
              Department= "chuquisaca",
              Province= "provincia",
              Municipality="municipalidad",
              WaterConnections= 30,
              ConnectionsWithMeter=40,
              ConnectionsWithoutMeter= 50,
              PublicPools= 60,
              Latrines=70,
              ServiceContinuity= "2h/dia"
          }}
        };

        private readonly ILogger<DummyController> _logger;

        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        // mobile

        [HttpPost]
        [Route("mobile/login")]
        public IActionResult login(LoginRequest request)
        {
            SupportUser currentUser = null;
            foreach (KeyValuePair<string, SupportUser> entry in users)
            {
                if (request.Password == entry.Value.Password && request.Username == entry.Value.UserName)
                {
                    currentUser = entry.Value;
                    break;
                }
            }
            if (currentUser == null)
            {
                throw new ArgumentException("No user");
            }
            return Ok(new LoginResponse()
            {
                Token = currentUser.id,
                KoboUserName = "super_admin",
                KoboPassword = "proagenda2030",
                KoboServerURL = "https://kc.nexion-dev.tk/"
            });
        }

        [HttpGet]
        [Route("mobile/profile")]
        public IActionResult profile()
        {
            string token = this.Request.Headers["token"];
            return Ok(users[token]);
        }

        [HttpPost]
        [Route("mobile/profile")]
        public IActionResult profile(SupportUser request)
        {
            string token = this.Request.Headers["token"];
            if (!users.ContainsKey(token))
                throw new ArgumentException("No user");
            users[token] = request;
            return Ok("ok");
        }

        // dashboard

        [HttpGet]
        [Route("dashboard/users")]
        public IEnumerable<SupportUser> account()
        {
            return users.Values.ToList();
        }

        [HttpPost]
        [Route("dashboard/user")]
        public IActionResult account(SupportUser request)
        {
            if (request.id == null)
            {
                Guid guid = Guid.NewGuid();
                request.id = guid.ToString();
            }
            
            if(users.ContainsKey(request.id))
                users[request.id] = request;
            else    
                users.Add(request.id, request);
            return Ok("ok");
        }
    }
}
