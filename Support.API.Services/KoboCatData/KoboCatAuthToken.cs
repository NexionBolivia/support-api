using System;

namespace Support.API.Services.KoboCatData
{
    public class KoboCatAuthToken
    {
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public int UserId { get; set; }
    }
}
