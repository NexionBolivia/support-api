namespace support_api.Models
{
    public class SupportUser
    {
        public string id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

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