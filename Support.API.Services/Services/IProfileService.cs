using Newtonsoft.Json.Linq;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Services
{
    public interface IProfileService
    {
        ProfileRequest GetProfile(int profileId);
        IEnumerable<ProfileRequest> GetProfiles();
        string CreateUpdateProfile(ProfileRequest data);
    }
}
