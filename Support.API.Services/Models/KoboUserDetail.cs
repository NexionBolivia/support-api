using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class KoboUserDetail
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public List<OrganizationSimple> Organizations { get; set; }
    }
}