using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace support_api.services.Models
{
    public class SupportApiUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdOrganization { get; set; }
        public Organization Organization { get; set; }

        public List<SupportApiUser_UserKobo> SupportApiUser_UserKobo { get; set; }
    }
}