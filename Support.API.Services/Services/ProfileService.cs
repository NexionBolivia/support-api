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

        public IEnumerable<ProfileRequest> GetProfiles(string userName)
        {
            List<ProfileRequest> response = new List<ProfileRequest>();

            /*if (userName != null)
            {// Get one profile action
                ProfileRequest profile = new ProfileRequest();
                SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == userName);

                if (user != null)
                {
                    response.Add(GetProfileData(user));
                }
            }
            else
            {
                List<SupportApiUser> users = context.SupportApiUser.ToList();
                foreach (SupportApiUser user in users)
                {
                    response.Add(GetProfileData(user));
                }
            }*/

            return response;
        }

        /*private ProfileRequest GetProfileData(SupportApiUser user)
        {
            ProfileRequest profile = new ProfileRequest();

            if (user != null)
            {
                profile.Username = user.Username;
                profile.Password = user.Password;

                Organization org = context.Organization.FirstOrDefault(x => x.IdOrganization == user.IdOrganization);
                if (org != null)
                {
                    profile.Name = org.Name;

                    //Get Profile Data
                    OrganizationProfile cProfile = context.Profile.FirstOrDefault(x => x.IdProfile == org.IdProfile);

                    if (cProfile != null)
                    {
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

            return profile;
        }*/

        public bool CreateProfile(ProfileRequest data)
        {
            bool response = false;

            try
            {
                if (data != null)
                {
                    // Save Profile
                    OrganizationProfile profile = new OrganizationProfile();
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
                    context.OrganizationProfiles.Add(profile);
                    context.SaveChanges();

                    // Save Organization
                    /*Organization org = new Organization();
                    org.Profile = profile;
                    org.Name = data.Name;
                    context.Organization.Add(org);
                    context.SaveChanges();


                    // Save User
                    SupportApiUser user = new SupportApiUser();
                    user.Username = data.Username;
                    user.Password = this.ToSha256(data.Password);
                    user.Organization = org;
                    context.SupportApiUser.Add(user);
                    context.SaveChanges();*/
                    response = true;
                }
            }
            catch
            {
                return response;
            }

            return response;
        }

        public bool UpdateProfile(ProfileRequest data)
        {
            bool response = false;

            try
            {
                /*if (data != null)
                {
                    SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == data.Username);

                    // Update SupportApi Data
                    user.Password = this.ToSha256(data.Password);
                    context.SupportApiUser.Update(user);
                    context.SaveChanges();

                    // Update Organization Data
                    Organization org = context.Organization.FirstOrDefault(x => x.IdOrganization == user.IdOrganization);
                    if (org != null)
                    {
                        org.Name = data.Name;
                        context.Organization.Update(org);
                        context.SaveChanges();

                        // Update Profile Data
                        OrganizationProfile profile = context.Profile.FirstOrDefault(x => x.IdProfile == org.IdProfile);
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
                            context.Profile.Update(profile);
                            context.SaveChanges();
                            response = true;
                        }
                    }
                }*/
            }
            catch
            {
                return response;
            }

            return response;
        }

        public LoginResponse Login(LoginRequest data)
        {
            LoginResponse resp = new LoginResponse();

            /*if (data != null && data.Username != null && data.Password != null)
            {
                if (data.Username != null && data.Password != null)
                {
                    SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == data.Username
                    && x.Password == this.ToSha256(data.Password));

                    if (user != null)
                    {
                        resp.Token = "AnyTokenValue";
                        resp.OdkServerUrl = "http://www.mylink.com";
                        resp.OdkUsername = "ValidOdkUsername";
                        resp.OdkPassword = "ValidOdkPassword";
                    }
                }
            }*/

            return resp;
        }

        private string ToSha256(string text)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
