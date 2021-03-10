using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using Support.API.Services.KoboData;
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

        public Task<List<RoleResponse>> GetAll()
        {
            var list = new List<RoleResponse>();
            foreach (Role rol in applicationDbContext.Roles)
            {
                list.Add(new RoleResponse()
                {
                    RoleId = rol.RoleId.ToString(),
                    Name = rol.Name
                });
            }

            return Task.FromResult(list);
        }
    }
}
