using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.Request
{
    public class ProfileRequest
    {
        public string ProfileId { get; set; }
        public string OrganizationId { get; set; }

        // Profile Fields
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
    }
}