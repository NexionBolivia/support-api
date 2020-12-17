using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SupportAPI.Services.Models
{
    public class Organization
    {
        public int IdOrganization { get; set; }
        public int IdProfile { get; set; }
        public Profile Profile { get; set; }
        public string Name { get; set; }

        public List<SupportApiUser> SupportApiUsers { get; set; }
    }
}