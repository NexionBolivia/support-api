using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.Request
{
    public class KoboUserRequest
    {
        public string Id { get; set; } //KoboUserId
        public List<string> Roles { get; set; }
        public List<string> Organizations { get; set; }
    }
}