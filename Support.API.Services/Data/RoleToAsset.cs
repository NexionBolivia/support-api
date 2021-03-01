using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class RoleToAsset
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}