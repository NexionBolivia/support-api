using Support.API.Services.KoboData;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace Support.API.Services.Services
{
    public interface IRoleService
    {
        Task<IList> GetAll();
    }
}