using Support.API.Services.KoboData;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;

namespace Support.API.Services.Services
{
    public interface IKoboUserService
    {
        IEnumerable<KoboUserDetail> GetAll();
        bool UpdateKoboUser(KoboUserRequest request);
    }
}