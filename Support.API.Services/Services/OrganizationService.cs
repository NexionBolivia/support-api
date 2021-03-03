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
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly KoboDbContext koboDbContext;

        public OrganizationService(ApplicationDbContext appContext, KoboDbContext koboContext)
        {
            this.applicationDbContext = appContext;
            this.koboDbContext = koboContext;
        }

        public bool CreateUpdateOrganization(OrganizationRequest request)
        {
            var response = false;
            if(response != null)
            {
                if (string.IsNullOrEmpty(request.OrganizationId)) //insert
                {
                    Organization org = new Organization();
                    
                    if(!string.IsNullOrEmpty(request.ParentOrganizationId))
                        org.ParentId = int.Parse(request.ParentOrganizationId);

                    org.Name = request.Name;
                    org.Color = request.Color;

                    if(!string.IsNullOrEmpty(request.ProfileId))
                        org.IdProfile = int.Parse(request.ProfileId);
                    
                    applicationDbContext.Organizations.Add(org);
                    applicationDbContext.SaveChanges();
                    SaveMembersForOrganization(org.OrganizationId, request.Members);
                    response = true;
                }
                else //update
                {
                    Organization org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == int.Parse(request.OrganizationId));
                    
                    if (!string.IsNullOrEmpty(request.ParentOrganizationId))
                        org.ParentId = int.Parse(request.ParentOrganizationId);
                    
                    org.Name = request.Name;
                    org.Color = request.Color;

                    if (!string.IsNullOrEmpty(request.ProfileId))
                        org.IdProfile = int.Parse(request.ProfileId);

                    applicationDbContext.Organizations.Update(org);
                    applicationDbContext.SaveChanges();
                    DeleteAllMembersFromOrganization(request.OrganizationId);
                    SaveMembersForOrganization(org.OrganizationId, request.Members);
                    response = true;
                }
            }

            return response;
        }

        public IEnumerable<OrganizationResponse> GetAll()
        {
            List<OrganizationResponse> list = new List<OrganizationResponse>();
            return list;
        }

        public bool DeleteOrganization(string organizationId)
        {
            bool response = false;

            return response;
        }

        private void DeleteAllMembersFromOrganization(string organizationId)
        {
            List<OrganizationToKoboUser> list = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.OrganizationId == int.Parse(organizationId)).ToList();
            applicationDbContext.OrganizationsToKoboUsers.RemoveRange(list);
            applicationDbContext.SaveChanges();
        }

        private void SaveMembersForOrganization(int organizationId, List<string> members)
        {
            if (members != null)
            {
                foreach (string id in members)
                {
                    OrganizationToKoboUser member = new OrganizationToKoboUser();
                    member.KoboUserId = int.Parse(id);
                    member.OrganizationId = organizationId;
                    applicationDbContext.OrganizationsToKoboUsers.Add(member);
                    applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
