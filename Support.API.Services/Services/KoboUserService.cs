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

namespace Support.API.Services.Services
{
    public class KoboUserService : IKoboUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly KoboDbContext koboDbContext;

        public KoboUserService(ApplicationDbContext appContext, KoboDbContext koboContext)
        {
            this.applicationDbContext = appContext;
            this.koboDbContext = koboContext;
        }

        public IEnumerable<KoboUserDetail> GetAll()
        {
            List<KoboUserDetail> response = new List<KoboUserDetail>();

            foreach(KoboUser kuser in koboDbContext.KoboUsers.ToList())
            {
                KoboUserDetail detail = new KoboUserDetail();
                detail.Id = kuser.Id.ToString();
                detail.Username = kuser.UserName;

                //Get Roles
                List<string> roleList = new List<string>();
                foreach(RoleToKoboUser role in applicationDbContext.RolesToKoboUsers.Where(x => x.KoboUserId == kuser.Id))
                {
                    roleList.Add(role.RoleId.ToString());
                }
                detail.Roles = roleList.ToArray();

                //Get Organizations
                List<OrganizationSimple> orgList = new List<OrganizationSimple>();
                List<OrganizationToKoboUser> orgKoboUserList = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.KoboUserId == kuser.Id).ToList();
                foreach (OrganizationToKoboUser orgToKobo in orgKoboUserList)
                {
                    Organization org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == orgToKobo.OrganizationId);
                    if(org != null)
                        orgList.Add(new OrganizationSimple() { 
                            OrganizationId = orgToKobo.OrganizationId.ToString(), Name = org.Name, Color = org.Color, ProfileId = org.IdProfile.ToString()
                        });
                }
                detail.Organizations = orgList;

                response.Add(detail);
            }

            return response;
        }

        public bool UpdateKoboUser(KoboUserRequest request)
        {
            bool response = false;
            return response;
        }
    }
}
