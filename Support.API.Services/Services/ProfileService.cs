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

            return response;
        }

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

            return response;
        }

        public LoginResponse Login(LoginRequest data)
        {
            LoginResponse resp = new LoginResponse();

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
