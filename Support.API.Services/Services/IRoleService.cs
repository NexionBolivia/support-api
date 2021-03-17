using Support.API.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support.API.Services.Services
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> GetAll();
    }
}