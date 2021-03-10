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
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Support.API.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RoleService(ApplicationDbContext appContext)
        {
            this.applicationDbContext = appContext;
        }

        public async Task<IList> GetAll()
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
