using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public Asset Parent { get; set; }
        public List<Asset> Children { get; set; }
        public List<RoleToAsset> RoleToAssets { get; set; }
    }
}