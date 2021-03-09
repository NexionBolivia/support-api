using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class OrganizationSimpleForLogin
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string ProfileId { get; set; }
        public List<OrganizationSimpleForLogin> Organizations { get; set; }
    }
}