using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Support.API.Services.Data;
using Support.API.Services.Models;

namespace SupportAPI.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;

        public ProfileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CompositeProfile> GetProfiles(JObject? data)
        {
            List<CompositeProfile> response = new List<CompositeProfile>();

            if (data != null && data.HasValues)
            {
                LoginData loginData = JsonConvert.DeserializeObject<LoginData>(data.ToString());
                if (loginData.Username != null)
                {// Get one profile action
                    CompositeProfile profile = new CompositeProfile();
                    SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == loginData.Username);

                    if (user != null)
                    {
                        response.Add(GetProfileData(user));
                    }
                }
            }
            else
            {
                List<SupportApiUser> users = context.SupportApiUser.ToList();
                foreach (SupportApiUser user in users)
                {
                    response.Add(GetProfileData(user));
                }
            }

            return response;
        }

        private CompositeProfile GetProfileData(SupportApiUser user)
        {
            CompositeProfile profile = new CompositeProfile();

            if (user != null)
            {
                profile.Username = user.Username;
                profile.Password = user.Password;

                Organization org = context.Organization.FirstOrDefault(x => x.IdOrganization == user.IdOrganization);
                if (org != null)
                {
                    profile.Name = org.Name;

                    //Get Profile Data
                    Profile cProfile = context.Profile.FirstOrDefault(x => x.IdProfile == org.IdProfile);

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
        }

        public bool CreateProfile(JObject? data)
        {
            bool response = false;

            if (data != null && data.HasValues)
            {
                CompositeProfile compProfile = JsonConvert.DeserializeObject<CompositeProfile>(data.ToString());

                if (compProfile != null)
                {
                    // Save Profile
                    Profile profile = new Profile();
                    profile.Formation = compProfile.Formation;
                    profile.Address = compProfile.Address;
                    profile.Phone = compProfile.Phone;
                    profile.Professionals = compProfile.Professionals;
                    profile.Employes = compProfile.Employes;
                    profile.Department = compProfile.Department;
                    profile.Province = compProfile.Province;
                    profile.Municipality = compProfile.Municipality;
                    profile.WaterConnections = compProfile.WaterConnections;
                    profile.ConnectionsWithMeter = compProfile.ConnectionsWithMeter;
                    profile.ConnectionsWithoutMeter = compProfile.ConnectionsWithoutMeter;
                    profile.PublicPools = compProfile.PublicPools;
                    profile.Latrines = compProfile.Latrines;
                    profile.ServiceContinuity = compProfile.ServiceContinuity;
                    context.Profile.Add(profile);
                    context.SaveChanges();

                    // Save Organization
                    Organization org = new Organization();
                    org.Profile = profile;
                    org.Name = compProfile.Name;
                    context.Organization.Add(org);
                    context.SaveChanges();


                    // Save User
                    SupportApiUser user = new SupportApiUser();
                    user.Username = compProfile.Username;
                    user.Password = this.ToSha256(compProfile.Password);
                    user.Organization = org;
                    context.SupportApiUser.Add(user);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }



        public bool UpdateProfile(JObject? data)
        {
            bool response = false;

            if (data != null && data.HasValues)
            {
                CompositeProfile compProfile = JsonConvert.DeserializeObject<CompositeProfile>(data.ToString());

                if (compProfile != null)
                {
                    SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == compProfile.Username);

                    // Update SupportApi Data
                    user.Password = this.ToSha256(compProfile.Password);
                    context.SupportApiUser.Update(user);
                    context.SaveChanges();

                    // Update Organization Data
                    Organization org = context.Organization.FirstOrDefault(x => x.IdOrganization == user.IdOrganization);
                    if (org != null)
                    {
                        org.Name = compProfile.Name;
                        context.Organization.Update(org);
                        context.SaveChanges();

                        // Update Profile Data
                        Profile profile = context.Profile.FirstOrDefault(x => x.IdProfile == org.IdProfile);
                        if (profile != null)
                        {
                            profile.Formation = compProfile.Formation;
                            profile.Address = compProfile.Address;
                            profile.Phone = compProfile.Phone;
                            profile.Professionals = compProfile.Professionals;
                            profile.Employes = compProfile.Employes;
                            profile.Department = compProfile.Department;
                            profile.Province = compProfile.Province;
                            profile.Municipality = compProfile.Municipality;
                            profile.WaterConnections = compProfile.WaterConnections;
                            profile.ConnectionsWithMeter = compProfile.ConnectionsWithMeter;
                            profile.ConnectionsWithoutMeter = compProfile.ConnectionsWithoutMeter;
                            profile.PublicPools = compProfile.PublicPools;
                            profile.Latrines = compProfile.Latrines;
                            profile.ServiceContinuity = compProfile.ServiceContinuity;
                            context.Profile.Update(profile);
                            context.SaveChanges();
                            response = true;
                        }
                    }
                }
            }

            return response;
        }

        public LoginResponse Login(JObject? data)
        {
            LoginResponse resp = new LoginResponse();

            if (data != null && data.HasValues)
            {
                LoginData loginData = JsonConvert.DeserializeObject<LoginData>(data.ToString());
                if (loginData.Username != null && loginData.Password != null)
                {
                    SupportApiUser user = context.SupportApiUser.FirstOrDefault(x => x.Username == loginData.Username
                    && x.Password == this.ToSha256(loginData.Password));

                    if (user != null)
                    {
                        resp.Token = "AnyTokenValue";
                        resp.OdkServerUrl = "http://www.mylink.com";
                        resp.OdkUsername = "ValidOdkUsername";
                        resp.OdkPassword = "ValidOdkPassword";
                    }
                }
            }

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
