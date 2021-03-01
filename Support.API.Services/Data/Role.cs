using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<RoleToAsset> RoleToAssets { get; set; }
        public List<RoleToKoboUser> RoleToKoboUsers { get; set; }
    }
}