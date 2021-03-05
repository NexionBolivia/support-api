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

        public OrganizationService(ApplicationDbContext appContext)
        {
            this.applicationDbContext = appContext;
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
            foreach (Organization organization in Enumerable.ToList<Organization>(this.applicationDbContext.Organizations))
            {
                list.Add(this.GetResponse(organization));
            }
            return list;
        }

        private OrganizationResponse GetResponse(Organization org)
        {
            OrganizationResponse organizationResponse = new OrganizationResponse()
            {
                OrganizationId = org.OrganizationId.ToString(),
                Name = org.Name,
                Color = org.Color,
                ProfileId = org.IdProfile != null ? org.IdProfile.ToString() : "",
                Organizations = new List<OrganizationResponse>(),
                Members = new List<string>()
            };

            if (org.ParentId != null && !string.IsNullOrEmpty(org.ParentId.ToString()))
            {
                List<Organization> orgs = applicationDbContext.Organizations.Where(x => x.OrganizationId == org.ParentId).ToList();
                foreach (Organization organization in orgs)
                {
                    organizationResponse.Organizations.Add(this.GetResponse(organization));
                }
            }

            List<OrganizationToKoboUser> koboUsers = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.OrganizationId == org.OrganizationId).ToList();
            foreach (OrganizationToKoboUser organizationToKoboUser in koboUsers)
            {
                organizationResponse.Members.Add(organizationToKoboUser.KoboUserId.ToString());
            }

            return organizationResponse;
        }

        public bool DeleteOrganization(string organizationId)
        {
            bool response = false;
            if(!string.IsNullOrEmpty(organizationId))
            {
                List<Organization> orgs = applicationDbContext.Organizations.Where(x => x.ParentId == int.Parse(organizationId)).ToList();
                applicationDbContext.Organizations.RemoveRange(orgs);
                applicationDbContext.SaveChanges();

                Organization org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == int.Parse(organizationId));
                applicationDbContext.Organizations.Remove(org);
                applicationDbContext.SaveChanges();
                response = true;
            }

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
