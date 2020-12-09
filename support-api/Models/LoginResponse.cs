namespace support_api.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string KoboUserName { get; set; }
        public string KoboPassword { get; set; }
        public string KoboServerURL { get; set; }
    }
}