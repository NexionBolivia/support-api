using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class OrganizationProfile
    {
        public int ProfileId { get; set; }
        public string Formation { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Professionals { get; set; }
        public int Employes { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public int WaterConnections { get; set; }
        public int ConnectionsWithMeter { get; set; }
        public int ConnectionsWithoutMeter { get; set; }
        public int PublicPools { get; set; }
        public int Latrines { get; set; }
        public string ServiceContinuity { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}