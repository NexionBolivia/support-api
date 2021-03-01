using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class OrganizationToKoboUser
    {
        public int KoboUserId { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}