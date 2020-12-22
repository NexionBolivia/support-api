using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
