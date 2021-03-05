using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Support.API.Services.Data;
using Support.API.Services.KoboData;
using Support.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support.API.Services.Extensions
{
    public static class SeedExtensions
    {
		public static void SeedData(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				if(dbContext != null)
					dbContext.SeedData();
			}
		}

		public static void SeedData(this ApplicationDbContext context, bool migrateDb = true)
		{
			if(context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory" && migrateDb)
				context.Database.Migrate();

			// Seed Data
			OrganizationProfile profile = new OrganizationProfile();
			Organization organization = new Organization();
			Role role = new Role();
			Asset asset = new Asset();
			RoleToKoboUser roleToKoboUser = new RoleToKoboUser();
			OrganizationToKoboUser organizationToKoboUser = new OrganizationToKoboUser();
			RoleToAsset roleToAsset = new RoleToAsset();
			roleToAsset.Asset = asset;
			roleToAsset.Role = role;
			List<RoleToAsset> listRoleToAsset = new List<RoleToAsset>();
			listRoleToAsset.Add(roleToAsset);

			if (!context.OrganizationProfiles.Any())
			{
				profile.Formation = "formation 1";
				profile.Address = "address 1";
				profile.Phone = "phone 1";
				profile.Professionals = 1;
				profile.Employes = 1;
				profile.Department = "department 1";
				profile.Province = "provicen 1";
				profile.Municipality = "municipality 1";
				profile.WaterConnections = 1;
				profile.ConnectionsWithMeter = 1;
				profile.ConnectionsWithoutMeter = 1;
				profile.PublicPools = 1;
				profile.Latrines = 1;
				profile.ServiceContinuity = "service continuity 1";
				context.OrganizationProfiles.Add(profile);
				context.SaveChanges();
			}
			if (!context.Organizations.Any())
			{
				organization.OrganizationProfile = profile;
				organization.Name = "CAPY 1";
				organization.Color = "Red";
				context.Organizations.Add(organization);
				context.SaveChanges();
			}
			if (!context.Roles.Any())
			{
				role.Name = "Role 1";
				context.Roles.Add(role);
				context.SaveChanges();
			}
			if (!context.Assets.Any())
			{
				asset.Name = "Role 1";
				asset.Path = "Path 1";
				asset.Type = "VIEW"; //MENU or VIEW
				asset.RoleToAssets = listRoleToAsset;
				context.Assets.Add(asset);
				context.SaveChanges();
			}
			if (!context.RolesToKoboUsers.Any())
			{
				roleToKoboUser.Role = role;
				roleToKoboUser.KoboUserId = 1; //admin
				context.RolesToKoboUsers.Add(roleToKoboUser);
				context.SaveChanges();
			}
			if (!context.OrganizationsToKoboUsers.Any())
			{
				organizationToKoboUser.Organization = organization;
				organizationToKoboUser.KoboUserId = 1; //admin
				context.OrganizationsToKoboUsers.Add(organizationToKoboUser);
				context.SaveChanges();
			}
		}
	}
}