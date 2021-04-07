namespace Support.API.Services.Models.Request
{
    public class SeedRequest
    {
        // Perform deletion of DB
        public bool DeleteDB { get; set; }
        // Performs DB migration
        public bool Migrate { get; set; }
    }
}
