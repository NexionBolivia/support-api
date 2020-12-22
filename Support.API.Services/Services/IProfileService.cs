using Newtonsoft.Json.Linq;
using Support.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportAPI.Services.Services
{
    public interface IProfileService
    {
        IEnumerable<CompositeProfile> GetProfiles(JObject? data);
        bool CreateProfile(JObject? data);
        bool UpdateProfile(JObject? data);
        LoginResponse Login(JObject? data);
    }
}
