using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Support.API.Services.Data;
using Support.API.Services.KoboCatData;
using Support.API.Services.KoboFormData;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.API.Services.Services
{
    public class KoboUserService : IKoboUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly KoboFormDbContext koboFormDbContext;
        private readonly KoboCatDbContext koboCatContext;
        private readonly ILogger<KoboUserService> logger;

        public KoboUserService(ApplicationDbContext appContext, 
            KoboFormDbContext koboFormContext,
            KoboCatDbContext koboCatContext,
            ILogger<KoboUserService> logger)
        {
            this.applicationDbContext = appContext;
            this.koboFormDbContext = koboFormContext;
            this.koboCatContext = koboCatContext;
            this.logger = logger;
        }

        public IEnumerable<KoboUserDetail> GetAll()
        {
            var response = new List<KoboUserDetail>();

            foreach(KoboUser kuser in koboFormDbContext.KoboUsers.ToList())
            {
                var detail = new KoboUserDetail();
                detail.Id = kuser.Id.ToString();
                detail.Username = kuser.UserName;

                //Get Roles
                detail.Roles = new List<string>();
                foreach(RoleToKoboUser role in applicationDbContext.RolesToKoboUsers.Where(x => x.KoboUserId == kuser.Id))
                {
                    detail.Roles.Add(role.RoleId.ToString());
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
                    }
                }
                response = true;
            }
            this.applicationDbContext.SaveChanges();
            return response;
        }

        public IEnumerable<OrganizationSimple> GetOrganizationsByKoboUsername(string username)
        {
            var response = new List<OrganizationSimple>();
            var kuser = koboFormDbContext.KoboUsers.Where(x => x.UserName == username).FirstOrDefault();

            if(kuser != null)
            {
                //Get Organizations
                var orgKoboUserList = applicationDbContext.OrganizationsToKoboUsers.Where(x => x.KoboUserId == kuser.Id).ToList();
                foreach (OrganizationToKoboUser orgToKobo in orgKoboUserList)
                {
                    var org = applicationDbContext.Organizations.FirstOrDefault(x => x.OrganizationId == orgToKobo.OrganizationId);
                    if (org != null)
                        response.Add(new OrganizationSimple()
                        {
                            OrganizationId = orgToKobo.OrganizationId.ToString(),
                            Name = org.Name,
                            Color = org.Color,
                            ProfileId = org.IdProfile.ToString()
                        });
                }
            }

            return response.OrderBy(x => x.Name);
        }

        public IEnumerable<string> GetRolesByKoboUsername(string username)
        {
            var response = new List<string>();
            var kuser = koboFormDbContext.KoboUsers.Where(x => x.UserName == username).FirstOrDefault();

            if (kuser != null)
            {
                var koboUsers = applicationDbContext.RolesToKoboUsers.Where(x => x.KoboUserId == kuser.Id).ToList();
                foreach (RoleToKoboUser user in koboUsers)
                {
                    var role = applicationDbContext.Roles.Where(x => x.RoleId == user.RoleId).FirstOrDefault();
                    if (role != null)
                    {
                        response.Add(role.RoleId.ToString());
                    }
                }
            }

            return response;
        }

        public async Task<List<UserAsset>> GetAssetsForCurrentUser(string userName)
        {
            var assets = new List<UserAsset>();

            var userRoleIds = GetRolesByKoboUsername(userName);

            foreach (var userRoleId in userRoleIds)
            {
                var currentRoleToAssets = await applicationDbContext
                                                    .RoleToAssets
                                                    .Include(p => p.Asset)
                                                    .Where(p => p.RoleId == Convert.ToInt32(userRoleId)).ToListAsync();

                foreach (var assetInRole in currentRoleToAssets)
                {
                    if ( ! assets.Any(p => p.AssetId == assetInRole.AssetId))
                    {
                        assets.Add(new UserAsset
                        {
                            AssetId = assetInRole.AssetId,
                            Name = assetInRole.Asset.Name,
                            ParentId = assetInRole.Asset.ParentId,
                            Path = assetInRole.Asset.Path,
                            Type = assetInRole.Asset.Type
                        });
                    }
                }
            }

            return assets;
        }

        public async Task<int> GetKoboUserIdForKoboUsername(string username)
        {
            int response=-1;
            var kuser = koboFormDbContext.KoboUsers.Where(x => x.UserName == username).FirstOrDefault();

            if (kuser != null) response = kuser.Id;

            return response;
        }

        public async Task<string> SetFirstLoginToken(string userName)
        {
            string key = null;
            var user = await koboFormDbContext.KoboUsers.FirstOrDefaultAsync(p => p.UserName == userName);

            if (user != null)
            {
                var tokenOnKoboCat = await koboCatContext.AuthTokens.FirstOrDefaultAsync(p => p.UserId == user.Id);
                var tokenOnKoboForm = await koboFormDbContext.AuthTokens.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (tokenOnKoboCat == null && tokenOnKoboForm == null)
                {
                    key = GenerateToken();
                    // Saves on KoboCat DB
                    tokenOnKoboCat = new KoboCatAuthToken
                    {
                        Created = DateTime.UtcNow,
                        UserId = user.Id,
                        Key = key
                    };
                    koboCatContext.Add(tokenOnKoboCat);
                    await koboCatContext.SaveChangesAsync();

                    // Saves on KoboForm DB
                    tokenOnKoboForm = new KoboFormAuthToken
                    {
                        Created = DateTime.UtcNow,
                        UserId = user.Id,
                        Key = key
                    };
                    koboFormDbContext.Add(tokenOnKoboForm);
                    await koboFormDbContext.SaveChangesAsync();

                    logger.LogInformation($"Tokens created on Kobo DBs for userId: {user.Id} ({userName})");
                }
                else if (
                        (tokenOnKoboCat == null && tokenOnKoboForm != null)
                    || (tokenOnKoboCat != null && tokenOnKoboForm == null))
                {
                    logger.LogError($"Error with authtoken_token table. User on KoboForm db: { (tokenOnKoboForm == null ? "Does not exist" : "Exist") }. User on KoboCat db: { (tokenOnKoboCat == null ? "Does not exist" : "Exist") }");
                    throw new ApplicationException($"Error with authtoken_token table. User on KoboForm db: { (tokenOnKoboForm == null ? "Does not exist" : "Exist") }. User on KoboCat db: { (tokenOnKoboCat == null ? "Does not exist" : "Exist") }");
                }
            }
            return key;
        }

        public async Task<string> ActivateUser(string userName)
        {
            var userDetail = new StringBuilder();
            userDetail.Append($"User: {userName}");

            userDetail.Append(", koboForm: ");
            var userOnKoboForm = await koboFormDbContext.KoboUsers.FirstOrDefaultAsync(p => p.UserName == userName);

            if (userOnKoboForm == null)
                userDetail.Append("NOT EXIST!");
            else {
                if (!userOnKoboForm.IsActive)
                {
                    userOnKoboForm.IsActive = true;
                    koboFormDbContext.Update(userOnKoboForm);
                    await koboFormDbContext.SaveChangesAsync();
                    userDetail.Append("updated, ");
                }
                userDetail.Append($"IsActive={ userOnKoboForm.IsActive }");
            }

            userDetail.Append(", koboCat: ");

            var userOnKoboCat = await koboCatContext.KoboCatUsers.FirstOrDefaultAsync(p => p.UserName == userName);

            if (userOnKoboCat == null)
                userDetail.Append("NOT EXIST!");
            else
            {
                if (!userOnKoboCat.IsActive)
                {
                    userOnKoboCat.IsActive = true;
                    koboCatContext.Update(userOnKoboCat);
                    await koboCatContext.SaveChangesAsync();
                    userDetail.Append("updated, ");
                }
                userDetail.Append($"IsActive={ userOnKoboCat.IsActive }");
            }

            return userDetail.ToString();
        }

        private string GenerateToken()
        {
            string key = null;
            using (var rijndael = System.Security.Cryptography.Rijndael.Create())
            {
                rijndael.GenerateKey();
                key = Convert.ToBase64String(rijndael.Key);
            }

            if(key.Length > 40)
                key = key.Substring(0, 40);

            return key;
        }

        private OrganizationSimpleForLogin GetOrganizationResponse(Organization org)
        {
            var organizationResponse = new OrganizationSimpleForLogin()
            {
                OrganizationId = org.OrganizationId.ToString(),
                Name = org.Name,
                Color = org.Color,
                ProfileId = org.IdProfile.ToString(),
                Organizations = new List<OrganizationSimpleForLogin>(),
            };

            var orgs = applicationDbContext.Organizations.Where(x => x.ParentId == org.OrganizationId).ToList();
            foreach (Organization organization in orgs)
            {
                organizationResponse.Organizations.Add(this.GetOrganizationResponse(organization));
            }

            organizationResponse.Organizations.Add(organizationResponse);

            return organizationResponse;
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