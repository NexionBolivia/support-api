using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class RoleToKoboUser
    {
        public int KoboUserId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}