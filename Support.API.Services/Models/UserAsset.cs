namespace Support.API.Services.Models
{
    public class UserAsset
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
    }
}
