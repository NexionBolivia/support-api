﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Support.API.Services.Data;
using Support.API.Services.Models;
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
			/*SupportApiUser supportApiUser = new SupportApiUser();
			SupportApiUser supportApiUser2 = new SupportApiUser();
			UserKobo userKoboAdmin = new UserKobo();
			UserKobo userKoboApi = new UserKobo();
			SupportApiUser_UserKobo saUk = new SupportApiUser_UserKobo();
			SupportApiUser_UserKobo saUk2 = new SupportApiUser_UserKobo();

			if (!context.Profile.Any())
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
				context.Profile.Add(profile);
				context.SaveChanges();
			}
			if (!context.Organization.Any())
			{
				organization.Profile = profile;
				organization.Name = "CAPY 1";
				context.Organization.Add(organization);
				context.SaveChanges();
			}
			if (!context.SupportApiUser.Any())
			{
				supportApiUser.Organization = organization;
				supportApiUser.Username = "Capy1";
				supportApiUser.Password = "Capy1";
				supportApiUser.SupportApiUser_UserKobo = new List<SupportApiUser_UserKobo>();

				supportApiUser2.Organization = organization;
				supportApiUser2.Username = "Capy2";
				supportApiUser2.Password = "Capy2";
				supportApiUser2.SupportApiUser_UserKobo = new List<SupportApiUser_UserKobo>();

				context.SupportApiUser.Add(supportApiUser);
				context.SupportApiUser.Add(supportApiUser2);
				context.SaveChanges();
			}
			if (!context.UserKobo.Any())
			{
				userKoboAdmin.Name = "super_admin";
				userKoboAdmin.Password = "super_admin";
				context.UserKobo.Add(userKoboAdmin);
				userKoboApi.Name = "support_api";
				userKoboApi.Password = "support_api";
				context.UserKobo.Add(userKoboApi);
				context.SaveChanges();
			}
			if (supportApiUser.SupportApiUser_UserKobo != null &&
				supportApiUser.SupportApiUser_UserKobo.Count == 0)
            {
				saUk.SupportApiUser = supportApiUser;
				saUk.UserKobo = userKoboAdmin;
				saUk2.SupportApiUser = supportApiUser;
				saUk2.UserKobo = userKoboApi;
				supportApiUser.SupportApiUser_UserKobo.Add(saUk);
				supportApiUser.SupportApiUser_UserKobo.Add(saUk2);
				context.SaveChanges();
			}*/
		}
	}
}