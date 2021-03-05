using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Color { get; set; }
        public Organization Parent { get; set; }
        public List<Organization> Children { get; set; }
        public List<OrganizationToKoboUser> OrganizationToKoboUsers { get; set; }
        public int? IdProfile { get; set; }
        public OrganizationProfile? OrganizationProfile { get; set; }
    }
}