using System;

namespace Support.API.Services.KoboFormData
{
    public class KoboFormAuthToken
    {
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public int UserId { get; set; }
    }
}
