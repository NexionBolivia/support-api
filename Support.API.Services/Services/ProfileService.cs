using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;

namespace Support.API.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;

        public ProfileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProfileRequest> GetProfiles()
        {
            var response = new List<ProfileRequest>();
            var users = context.OrganizationProfiles.ToList();
            foreach (OrganizationProfile user in users)
            {
                response.Add(GetProfileData(user));
            }

            return response;
        }

        public ProfileRequest GetProfile(int profileId)
        {
            var response = new ProfileRequest();

            if (profileId != null && profileId > 0)
            {
                var user = context.OrganizationProfiles.FirstOrDefault(x => x.ProfileId == profileId);
                if (user != null)
                {
                    response = GetProfileData(user);
                }
            }

            return response;
        }

        private ProfileRequest GetProfileData(OrganizationProfile user)
        {
            var profile = new ProfileRequest();

            if (user != null)
            {
                var org = context.Organizations.FirstOrDefault(x => x.OrganizationId == user.OrganizationId);
                if (org != null)
                {
                    profile.OrganizationId = org.OrganizationId.ToString();
                }
                else profile.OrganizationId = "0";

                //Get Profile Data
                var cProfile = context.OrganizationProfiles.FirstOrDefault(x => x.ProfileId == user.ProfileId);
                if (cProfile != null)
                {
                    profile.ProfileId = cProfile.ProfileId.ToString();
                    profile.Formation = cProfile.Formation;
                    profile.Address = cProfile.Address;
                    profile.Phone = cProfile.Phone;
                    profile.Professionals = cProfile.Professionals;
                    profile.Employes = cProfile.Employes;
                    profile.Department = cProfile.Department;
                    profile.Province = cProfile.Province;
                    profile.Municipality = cProfile.Municipality;
                    profile.WaterConnections = cProfile.WaterConnections;
                    profile.ConnectionsWithMeter = cProfile.ConnectionsWithMeter;
                    profile.ConnectionsWithoutMeter = cProfile.ConnectionsWithoutMeter;
                    profile.PublicPools = cProfile.PublicPools;
                    profile.Latrines = cProfile.Latrines;
                    profile.ServiceContinuity = cProfile.ServiceContinuity;
                }
            }

            return profile;
        }

        public string CreateUpdateProfile(ProfileRequest data)
        {
            var response = string.Empty;
            if(data != null)
            {
                if(string.IsNullOrEmpty(data.ProfileId))
                {
                    var profile = new OrganizationProfile();
                    profile.Formation = data.Formation;
                    profile.Address = data.Address;
                    profile.Phone = data.Phone;
                    profile.Professionals = data.Professionals;
                    profile.Employes = data.Employes;
                    profile.Department = data.Department;
                    profile.Province = data.Province;
                    profile.Municipality = data.Municipality;
                    profile.WaterConnections = data.WaterConnections;
                    profile.ConnectionsWithMeter = data.ConnectionsWithMeter;
                    profile.ConnectionsWithoutMeter = data.ConnectionsWithoutMeter;
                    profile.PublicPools = data.PublicPools;
                    profile.Latrines = data.Latrines;
                    profile.ServiceContinuity = data.ServiceContinuity;

                    if (!string.IsNullOrEmpty(data.OrganizationId))
                    {
                        profile.OrganizationId = int.Parse(data.OrganizationId);
                    }

                    context.OrganizationProfiles.Add(profile);
                    context.SaveChanges();

                    if (!string.IsNullOrEmpty(data.OrganizationId))
                    {
                        var org = context.Organizations.Where(x => x.OrganizationId == int.Parse(data.OrganizationId)).FirstOrDefault();
                        if (org != null)
                        {
                            org.IdProfile = profile.ProfileId;
                            context.Organizations.Update(org);
                            context.SaveChanges();
                        }
                    }
                    response = profile.ProfileId.ToString();
                }
                else
                {
                    var profile = context.OrganizationProfiles.FirstOrDefault(x => x.ProfileId == int.Parse(data.ProfileId));
                    if (profile != null)
                    {
                        profile.Formation = data.Formation;
                        profile.Address = data.Address;
                        profile.Phone = data.Phone;
                        profile.Professionals = data.Professionals;
                        profile.Employes = data.Employes;
                        profile.Department = data.Department;
                        profile.Province = data.Province;
                        profile.Municipality = data.Municipality;
                        profile.WaterConnections = data.WaterConnections;
                        profile.ConnectionsWithMeter = data.ConnectionsWithMeter;
                        profile.ConnectionsWithoutMeter = data.ConnectionsWithoutMeter;
                        profile.PublicPools = data.PublicPools;
                        profile.Latrines = data.Latrines;
                        profile.ServiceContinuity = data.ServiceContinuity;
                        context.OrganizationProfiles.Update(profile);
                        context.SaveChanges();
                        response = profile.ProfileId.ToString();
                    }
                }
            }

            return response;
        }
    }
}
