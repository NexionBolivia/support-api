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

namespace Support.Api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            //_httpClient = httpClient;
            _config = config;
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
            
            // TODO: Call a service to get User's Roles and Organizations

            return Ok(new 
            { 
                id = data.Username,
                authToken = jwt,
                // TODO: Please fill these up with User's Roles and Organizations
                roles = new List<string>(),
                organizations = new List<string>()
                // organizations = List<OrganizationForUser>()
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
                var koboLoginUrl = _config.GetSection("KoBoToolboxUri").Value.ReplaceKoboToolboxUri();
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
