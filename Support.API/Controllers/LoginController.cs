using Microsoft.AspNetCore.Http;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Support.API.Services.Helpers;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Support.Api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IKoboUserService koboUserService;
        private readonly ILogger<LoginController> logger;

        public LoginController(IConfiguration config, 
            IKoboUserService koboUserService,
            ILogger<LoginController> logger)
        {
            _config = config;
            this.koboUserService = koboUserService;
            this.logger = logger;
        }

        /// <summary>
        ///     Authentication on KoBoToolbox. If it's user's first login, then it will 
        ///     create a token on authtoken_token tables on koboform and kobocat DBs.
        ///     This process assumes that is_active = true on auth_user tables on both DBs
        /// </summary>
        /// <response code="200">Annonymous type: { authToken: string} </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate(LoginRequest data)
        {
            var koboToken = await LoginToKoBoToolbox(data, firstAttempt: true);

            if (koboToken == null)
                return Unauthorized();

            // Create JWT 
            var jwt = CreateJWT(data, koboToken);
            
            return Ok(new 
            {
                authToken = jwt
            });
        }

        private string CreateJWT(LoginRequest data, string koboToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, data.Username),
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

        private async Task<string> LoginToKoBoToolbox(LoginRequest request, bool firstAttempt)
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
                // Does the best effort for login
                else if (responseMessage.StatusCode == System.Net.HttpStatusCode.Forbidden
                    && firstAttempt)
                {
                    logger.LogInformation($"Login user for first time: { request.Username }");

                    var key = await koboUserService.SetFirstLoginToken(request.Username);
                    if (key != null) // Means token was created 
                    {
                        firstAttempt = false;
                        logger.LogInformation($"ReLogin for user: { request.Username }");
                        token = await LoginToKoBoToolbox(request, firstAttempt);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when accessing any of Kobo DBs", request.Username);
               // Do not throw any exception here 
            }
            return token;
        }
    }
}
