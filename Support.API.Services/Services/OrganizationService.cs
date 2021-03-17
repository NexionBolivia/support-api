using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;
using System.Linq;

namespace Support.API.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrganizationService(ApplicationDbContext appContext)
        {
            this.applicationDbContext = appContext;
        }

        public string CreateUpdateOrganization(OrganizationRequest request)
        {
            var response = string.Empty;
            if(response != null)
            {
                if (string.IsNullOrEmpty(request.OrganizationId)) //insert
                {
                    var org = new Organization();
                    
                    if(!string.IsNullOrEmpty(request.ParentOrganizationId))
                        org.ParentId = int.Parse(request.ParentOrganizationId);

                    org.Name = request.Name;
                    org.Color = request.Color;

                    if(!string.IsNullOrEmpty(request.ProfileId))
                        org.IdProfile = int.Parse(request.ProfileId);
                    
                    applicationDbContext.Organizations.Add(org);
                    SaveMembersForOrganization(org.OrganizationId, request.Members);
                    applicationDbContext.SaveChanges();
                    response = org.OrganizationId.ToString();
                }
                else //update
                {
                    var org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == int.Parse(request.OrganizationId));
                    
                    if (!string.IsNullOrEmpty(request.ParentOrganizationId))
                        org.ParentId = int.Parse(request.ParentOrganizationId);
                    
                    org.Name = request.Name;
                    org.Color = request.Color;

                    if (!string.IsNullOrEmpty(request.ProfileId))
                        org.IdProfile = int.Parse(request.ProfileId);

                    applicationDbContext.Organizations.Update(org);
                    DeleteAllMembersFromOrganization(request.OrganizationId);
                    SaveMembersForOrganization(org.OrganizationId, request.Members);
                    applicationDbContext.SaveChanges();
                    response = org.OrganizationId.ToString();
                }
            }
            
            return response;
        }

        public IEnumerable<OrganizationResponse> GetAll()
        {
            var list = new List<OrganizationResponse>();
            foreach (Organization organization in Enumerable.ToList<Organization>(this.applicationDbContext.Organizations))
            {
                list.Add(this.GetResponse(organization));
            }
            return list;
        }

        private OrganizationResponse GetResponse(Organization org)
        {
            var organizationResponse = new OrganizationResponse()
            {
                OrganizationId = org.OrganizationId.ToString(),
                Name = org.Name,
                Color = org.Color,
                ProfileId = org.IdProfile != null ? org.IdProfile.ToString() : null,
                Organizations = new List<OrganizationResponse>(),
                Members = new List<string>()
            };

            //Organizations
            var orgs = applicationDbContext.Organizations.Where(x => x.ParentId == org.OrganizationId).ToList();
            foreach (Organization organization in orgs)
            {
                organizationResponse.Organizations.Add(this.GetResponse(organization));
            }

            //Members
            var koboUsers = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.OrganizationId == org.OrganizationId).ToList();
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
                var orgs = applicationDbContext.Organizations.Where(x => x.ParentId == int.Parse(organizationId)).ToList();
                foreach(Organization corg in orgs)
                {
                    if(corg.IdProfile != null)
                    {
                        var profile = applicationDbContext.OrganizationProfiles.FirstOrDefault(x => x.ProfileId == corg.IdProfile);
                        if(profile != null)
                        {
                            applicationDbContext.OrganizationProfiles.Remove(profile);
                        }
                    }
                }

                applicationDbContext.Organizations.RemoveRange(orgs);

                var org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == int.Parse(organizationId));
                if(org != null)
                {
                    if (org.IdProfile != null)
                    {
                        var profileOrg = applicationDbContext.OrganizationProfiles.FirstOrDefault(x => x.ProfileId == org.IdProfile);
                        if (profileOrg != null)
                        {
                            applicationDbContext.OrganizationProfiles.Remove(profileOrg);
                        }
                    }

                    applicationDbContext.Organizations.Remove(org);
                }
                
                applicationDbContext.SaveChanges();
                response = true;
            }

            return response;
        }

        private void DeleteAllMembersFromOrganization(string organizationId)
        {
            var list = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.OrganizationId == int.Parse(organizationId)).ToList();
            applicationDbContext.OrganizationsToKoboUsers.RemoveRange(list);
        }

        private void SaveMembersForOrganization(int organizationId, List<string> members)
        {
            if (members != null)
            {
                foreach (string id in members)
                {
                    var member = new OrganizationToKoboUser();
                    member.KoboUserId = int.Parse(id);
                    member.OrganizationId = organizationId;
                    applicationDbContext.OrganizationsToKoboUsers.Add(member);
                }
            }
        }
    }
}
