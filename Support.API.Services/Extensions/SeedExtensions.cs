using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Support.API.Services.Data;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;
using System.Linq;

namespace Support.API.Services.Extensions
{
    public static class SeedExtensions
    {
		public static void SeedData(this ApplicationDbContext context, ILogger logger, SeedRequest request)
		{
			if (!context.IsInMemory() &&
				request != null &&
				request.Migrate)
			{
				logger.LogInformation("Starting Schema migration - Support API");

				if (request.DeleteDB)
                {
					logger.LogInformation("Performing EnsureDeleted as requested");
					context.Database.EnsureDeleted();
					logger.LogInformation("EnsureDeleted performed");
				}
								
				context.Database.Migrate();
				logger.LogInformation("Migration performed");
			}

			// Seed Data
			if (!context.Organizations.Any() && !context.OrganizationsToKoboUsers.Any())
			{
				logger.LogInformation("Starting Data Seed for Organizations  - Support API");
				// Members
				var organization = new Organization()
				{
					OrganizationProfile = null,
					Name = "ProAgenda2030",
					Color = "#FF6900"
				};
				context.Organizations.Add(organization);
				context.OrganizationsToKoboUsers.Add(new OrganizationToKoboUser()
				{
					Organization = organization,
					KoboUserId = 1
				});
			}

			if (!context.Assets.Any() && !context.Roles.Any() && !context.RolesToKoboUsers.Any())
			{
				logger.LogInformation("Starting Data Seed for Assets, Roles & RolesToKoboUsers - Support API");
				// Roles
				var r_Administrador = new Role()
				{
					RoleId = 1,
					Name = "Administrador"
				};
				context.Roles.Add(r_Administrador);
				var r_admin_GAM = new Role()
				{
					RoleId = 2,
					Name = "Administrador GAM"
				};
				context.Roles.Add(r_admin_GAM);
				var r_solo_vista_GAM = new Role()
				{
					RoleId = 3,
					Name = "Solo Vista GAM"
				};
				context.Roles.Add(r_solo_vista_GAM);
				var r_admin_CAPY = new Role()
				{
					RoleId = 4,
					Name = "Administrador CAPY"
				};
				context.Roles.Add(r_admin_CAPY);
				var r_solo_vista_CAPY = new Role()
				{
					RoleId = 5,
					Name = "Solo Vista CAPY"
				};
				context.Roles.Add(r_solo_vista_CAPY);

				// Assets
				var a_forms_view = new Asset()
				{
					AssetId = 1,
					Name = "forms_view",
					Path = "",
					Type = ""
				};
				var a_library_view = new Asset()
				{
					AssetId = 2,
					Name = "library_view",
					Path = "",
					Type = ""
				};
				var a_organizations_view = new Asset()
				{
					AssetId = 3,
					Name = "organizations_view",
					Path = "",
					Type = ""
				};
				var a_organizations_create = new Asset()
				{
					AssetId = 4,
					Name = "organizations_create",
					Path = "",
					Type = ""
				};
				var a_organizations_update = new Asset()
				{
					AssetId = 5,
					Name = "organizations_update",
					Path = "",
					Type = ""
				};
				var a_organizations_remove = new Asset()
				{
					AssetId = 6,
					Name = "organizations_remove",
					Path = "",
					Type = ""
				};
				var a_organizations_profile_view = new Asset()
				{
					AssetId = 7,
					Name = "organizations_profile_view",
					Path = "",
					Type = ""
				};
				var a_organizations_profile_create = new Asset()
				{
					AssetId = 8,
					Name = "organizations_profile_create",
					Path = "",
					Type = ""
				};
				var a_organizations_profile_update = new Asset()
				{
					AssetId = 9,
					Name = "organizations_profile_update",
					Path = "",
					Type = ""
				};
				var a_organizations_users_view = new Asset()
				{
					AssetId = 10,
					Name = "organizations_users_view",
					Path = "",
					Type = ""
				};
				var a_organizations_users_add = new Asset()
				{
					AssetId = 11,
					Name = "organizations_users_add",
					Path = "",
					Type = ""
				};
				var a_organizations_users_remove = new Asset()
				{
					AssetId = 12,
					Name = "organizations_users_remove",
					Path = "",
					Type = ""
				};
				var a_users_view = new Asset()
				{
					AssetId = 13,
					Name = "users_view",
					Path = "",
					Type = ""
				};
				var a_users_update = new Asset()
				{
					AssetId = 14,
					Name = "users_update",
					Path = "",
					Type = ""
				};
				var a_analytics_view = new Asset()
				{
					AssetId = 15,
					Name = "analytics_view",
					Path = "",
					Type = ""
				};

				// Roles-Assets
				a_forms_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_forms_view, Role = r_Administrador }
				};
				context.Assets.Add(a_forms_view);

				a_library_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_library_view, Role = r_Administrador }
				};
				context.Assets.Add(a_library_view);

				a_organizations_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_view, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_view, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_view, Role = r_solo_vista_GAM },
					new RoleToAsset() { Asset = a_organizations_view, Role = r_admin_CAPY },
					new RoleToAsset() { Asset = a_organizations_view, Role = r_solo_vista_CAPY }
				};
				context.Assets.Add(a_organizations_view);

				a_organizations_create.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_create, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_create, Role = r_admin_GAM }
				};
				context.Assets.Add(a_organizations_create);

				a_organizations_update.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_update, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_update, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_update, Role = r_admin_CAPY }
				};
				context.Assets.Add(a_organizations_update);

				a_organizations_remove.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_remove, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_remove, Role = r_admin_GAM }
				};
				context.Assets.Add(a_organizations_remove);

				a_organizations_profile_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_profile_view, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_profile_view, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_profile_view, Role = r_solo_vista_GAM },
					new RoleToAsset() { Asset = a_organizations_profile_view, Role = r_admin_CAPY },
					new RoleToAsset() { Asset = a_organizations_profile_view, Role = r_solo_vista_CAPY }
				};
				context.Assets.Add(a_organizations_profile_view);

				a_organizations_profile_create.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_profile_create, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_profile_create, Role = r_admin_GAM }
				};
				context.Assets.Add(a_organizations_profile_create);

				a_organizations_profile_update.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_profile_update, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_profile_update, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_profile_update, Role = r_admin_CAPY }
				};
				context.Assets.Add(a_organizations_profile_update);

				a_organizations_users_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_users_view, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_users_view, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_users_view, Role = r_solo_vista_GAM },
					new RoleToAsset() { Asset = a_organizations_users_view, Role = r_admin_CAPY },
					new RoleToAsset() { Asset = a_organizations_users_view, Role = r_solo_vista_CAPY }
				};
				context.Assets.Add(a_organizations_users_view);

				a_organizations_users_add.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_users_add, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_users_add, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_users_add, Role = r_admin_CAPY },
				};
				context.Assets.Add(a_organizations_users_add);

				a_organizations_users_remove.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_organizations_users_remove, Role = r_Administrador },
					new RoleToAsset() { Asset = a_organizations_users_remove, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_organizations_users_remove, Role = r_admin_CAPY },
				};
				context.Assets.Add(a_organizations_users_remove);

				a_users_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_users_view, Role = r_Administrador },
					new RoleToAsset() { Asset = a_users_view, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_users_view, Role = r_admin_CAPY }
				};
				context.Assets.Add(a_users_view);

				a_users_update.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_users_update, Role = r_Administrador },
					new RoleToAsset() { Asset = a_users_update, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_users_update, Role = r_admin_CAPY }
				};
				context.Assets.Add(a_users_update);

				a_analytics_view.RoleToAssets = new List<RoleToAsset>() {
					new RoleToAsset() { Asset = a_analytics_view, Role = r_Administrador },
					new RoleToAsset() { Asset = a_analytics_view, Role = r_admin_GAM },
					new RoleToAsset() { Asset = a_analytics_view, Role = r_solo_vista_GAM },
					new RoleToAsset() { Asset = a_analytics_view, Role = r_admin_CAPY },
					new RoleToAsset() { Asset = a_analytics_view, Role = r_solo_vista_CAPY }
				};
				context.Assets.Add(a_analytics_view);

				//Roles to Kobo User
				context.RolesToKoboUsers.Add(new RoleToKoboUser()
				{
					Role = r_Administrador,
					KoboUserId = 1
				});

				context.SaveChanges();
				logger.LogInformation("Data seed generated for Support API");
			}
		}
		private static bool IsInMemory(this ApplicationDbContext context) => context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
	}
}