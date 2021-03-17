using Microsoft.EntityFrameworkCore;
using Support.API.Services.Data;
using Support.API.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support.API.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RoleService(ApplicationDbContext appContext)
        {
            this.applicationDbContext = appContext;
        }

        public async Task<List<RoleResponse>> GetAll()
        {
            var list = new List<RoleResponse>();
            var roles = await applicationDbContext.Roles.ToListAsync();
            foreach (Role rol in roles)
            {
                list.Add(new RoleResponse()
                {
                    RoleId = rol.RoleId.ToString(),
                    Name = rol.Name
                });
            }

            return list;
        }
    }
}
