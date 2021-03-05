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
            var response = new List<KoboUserDetail>();

            foreach(KoboUser kuser in koboDbContext.KoboUsers.ToList())
            {
                var detail = new KoboUserDetail();
                detail.Id = kuser.Id.ToString();
                detail.Username = kuser.UserName;

                //Get Roles
                var roleList = new List<string>();
                foreach(RoleToKoboUser role in applicationDbContext.RolesToKoboUsers.Where(x => x.KoboUserId == kuser.Id))
                {
                    roleList.Add(role.RoleId.ToString());
                }

                //Get Organizations
                var orgList = new List<OrganizationSimple>();
                var orgKoboUserList = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.KoboUserId == kuser.Id).ToList();
                foreach (OrganizationToKoboUser orgToKobo in orgKoboUserList)
                {
                    var org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == orgToKobo.OrganizationId);
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
            var response = false;
            if ((request == null ? false : !String.IsNullOrEmpty(request.Id)))
            {
                if ((object)request.Roles != (object)null)
                {
                    this.DeleteAllRolesFromKoboUser(request.Id);
                    if (request.Roles.Count > 0)
                    {
                        foreach (string role in request.Roles)
                        {
                            this.applicationDbContext.RolesToKoboUsers.Add(new RoleToKoboUser
                            {
                                KoboUserId = Int32.Parse(request.Id),
                                RoleId = Int32.Parse(role)
                            });
                        }
                        response = true;
                    }
                }
                if ((object)request.Organizations != (object)null)
                {
                    this.DeleteAllOrganizationsFromKoboUser(request.Id);
                    if (request.Organizations.Count > 0)
                    {
                        foreach (string organization in request.Organizations)
                        {
                            this.applicationDbContext.OrganizationsToKoboUsers.Add(new OrganizationToKoboUser()
                            {
                                KoboUserId = Int32.Parse(request.Id),
                                OrganizationId = Int32.Parse(organization)
                            });
                        }
                        response = true;
                    }
                }
            }
            this.applicationDbContext.SaveChanges();
            return response;
        }

        private void DeleteAllOrganizationsFromKoboUser(string koboUserId)
        {
            if (!String.IsNullOrEmpty(koboUserId))
            {
                var list = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.KoboUserId == Convert.ToInt32(koboUserId)).ToList();
                this.applicationDbContext.OrganizationsToKoboUsers.RemoveRange(list);
            }
        }

        private void DeleteAllRolesFromKoboUser(string koboUserId)
        {
            if (!String.IsNullOrEmpty(koboUserId))
            {
                var list = applicationDbContext.RolesToKoboUsers.Where(x => x.KoboUserId == Convert.ToInt32(koboUserId)).ToList();
                this.applicationDbContext.RolesToKoboUsers.RemoveRange(list);
            }
        }
    }
}
