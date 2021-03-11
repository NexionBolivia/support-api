using Support.API.Services.KoboData;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support.API.Services.Services
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetAll();
    }
}