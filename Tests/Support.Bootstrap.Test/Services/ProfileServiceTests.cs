using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using Support.API.Services.Services;
using Support.API.Services.Models.Request;

namespace Support.API.Services.Test.Services
{
    public class ProfileServiceTests
    {
        private ApplicationDbContext context;
        private IProfileService profileService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            SeedExtensions.SeedData(context);
            profileService = new ProfileService(context);
        }

        [TearDown]
        public void TearDown()
        {
            profileService = null;
            context = null;
        }

        [Test]
        public void GetProfiles_Empty_Username_Test()
        {
            Assert.AreEqual(0, profileService.GetProfiles("").Count());
        }

        [Test]
        public void GetProfiles_Wrong_Username_Test()
        {
            Assert.AreEqual(0, profileService.GetProfiles("Capy5").Count());
        }

        [Test]
        public void GetProfiles_Selected_Username_Test()
        {
            Assert.AreEqual(1, profileService.GetProfiles("Capy1").Count());
        }

        [Test]
        public void GetProfiles_Select_All_Test()
        {
            Assert.Greater(profileService.GetProfiles(null).Count(), 1);
        }

        [Test]
        public void CreateProfile_Result_True_Test()
        {
            ProfileRequest request = new ProfileRequest() { 
                Username = "javier", // for SupportApiUser
                Password = "javier", // for SupportApiUser
                Name = "Capy4",      // name of the new Organization
                Formation = "Form1", // below data for new profile
                Address = "Addr1",
                Phone = "123456",
                Professionals = 1,
                Employes = 1,
                Department = "Dep1",
                Province = "Prov1",
                Municipality = "Mun1",
                WaterConnections = 1,
                ConnectionsWithMeter = 1,
                ConnectionsWithoutMeter = 1,
                PublicPools = 1,
                Latrines = 1,
                ServiceContinuity = "ServCont1"
            };
            Assert.AreEqual(profileService.CreateProfile(request), true);
        }

        [Test]
        public void CreateProfile_Result_False_Test()
        {
            Assert.AreEqual(profileService.CreateProfile(null), false);
        }

        [Test]
        public void UpdateProfile_Result_True_Test()
        {
            ProfileRequest request = new ProfileRequest()
            {
                Username = "Capy1",          // for SupportApiUser
                Password = "Capy1-new-pass", // for SupportApiUser
                Name = "Capy5",              // name of the new Organization
                Formation = "Form1",         // below data for new profile
                Address = "Addr2",
                Phone = "1234567",
                Professionals = 1,
                Employes = 1,
                Department = "Dep2",
                Province = "Prov2",
                Municipality = "Mun2",
                WaterConnections = 1,
                ConnectionsWithMeter = 1,
                ConnectionsWithoutMeter = 1,
                PublicPools = 1,
                Latrines = 1,
                ServiceContinuity = "ServCont2"
            };
            Assert.AreEqual(profileService.UpdateProfile(request), true);
        }

        [Test]
        public void UpdateProfile_Result_False_Test()
        {
            Assert.AreEqual(profileService.UpdateProfile(null), false);
        }

        [Test]
        public void Login_Result_Ok_Test()
        {
            LoginRequest request = new LoginRequest()
            {
                Username = "Capy1",
                Password = "Capy1"
            };
            Assert.AreNotEqual(profileService.Login(request), null);
        }

        [Test]
        public void Login_Wrong_Password_Test()
        {
            LoginRequest request = new LoginRequest()
            {
                Username = "Capy1",
                Password = "Capy123"
            };
            Assert.AreEqual(profileService.Login(request).Token, null);
        }

        [Test]
        public void Login_Empty_Data_Test()
        {
            Assert.AreEqual(profileService.Login(null).Token, null);
        }
    }
}