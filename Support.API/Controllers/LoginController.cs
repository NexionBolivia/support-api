using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Support.API.Services.Models.Request;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Support.API.Services.Helpers;
using System.IO;
using Support.API.Services.Data;
using Support.API.Services.Services;
using Support.API.Services.KoboData;

namespace Support.Api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IKoboUserService koboUserService;

        public LoginController(IConfiguration config, IKoboUserService koboUserService)
        {
            _config = config;
            this.koboUserService = koboUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate(LoginRequest data)
        {
            var koboToken = await LoginToKoBoToolbox(data);

            if (koboToken == null)
                return Unauthorized();

            // Create JWT 
            var jwt = CreateJWT(data, koboToken);
            
            return Ok(new 
            { 
                id = data.Username,
                authToken = jwt,
                roles = this.koboUserService.GetRolesByKoboUsername(data.Username),
                organizations = this.koboUserService.GetOrganizationsByKoboUsername(data.Username)
            });
        }

        private string CreateJWT(LoginRequest data, string koboToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, data.Username),
                new Claim(ClaimTypes.UserData, koboToken)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("jwtToken").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<string> LoginToKoBoToolbox(LoginRequest request)
        {
            string token = null;
            try
            {
                var koboLoginUrl = _config.GetSection("KoBoToolboxUri").Value.ReplaceKoboToolboxUri().UrlCombine("token/?format=json");
                var message = new HttpRequestMessage(HttpMethod.Get, koboLoginUrl);

                var creds = Convert.ToBase64String(
                                Encoding.GetEncoding("ISO-8859-1")
                                        .GetBytes($"{request.Username}:{request.Password}"));

                message.Headers.Add("Authorization", new[] { $"Basic {creds}" });

                var httpClient = new HttpClient();
                var responseMessage = await httpClient.SendAsync(message);

                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var tokenRaw = await responseMessage.Content.ReadAsStringAsync();
                    var tokenTmpl = new { token = string.Empty };
                    var tokenObj = JsonConvert.DeserializeAnonymousType(tokenRaw, tokenTmpl);
                    
                    token = tokenObj.token;
                }
            }
            catch
            {
               // Do not throw any exception here 
            }
            return token;
        }
    }
}
