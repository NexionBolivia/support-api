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
        IEnumerable<ProfileRequest> GetProfiles(GetProfileRequest data);
        bool CreateProfile(ProfileRequest data);
        bool UpdateProfile(ProfileRequest data);
        LoginResponse Login(LoginRequest data);
    }
}
