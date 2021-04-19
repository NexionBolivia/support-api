using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Support.API.Services.Data;
using Support.API.Services.Extensions;
using Support.API.Services.Models.Request;
using Support.API.Services.Services;
using System;
using System.Linq;

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
            SeedExtensions.SeedData(context, Substitute.For<ILogger>(), Substitute.For<SeedRequest>());
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
            profileService.GetProfiles().Count().Should().Be(0);
        }

        [Test]
        public void GetProfiles_Wrong_Username_Test()
        {
            profileService.GetProfile(10).ProfileId.Should().Be("0");
        }

        [Test]
        public void GetProfiles_Select_All_Test()
        {
            profileService.GetProfiles().Count().Should().BeGreaterThan(1);
        }

        [Test]
        public void CreateProfile_Result_True_Test()
        {
            ProfileRequest request = new ProfileRequest() {
                ProfileId = "1",
                OrganizationId = "1",
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
            profileService.CreateUpdateProfile(request).Should().Be("1");
        }

        [Test]
        public void CreateProfile_Result_False_Test()
        {
            profileService.CreateUpdateProfile(null).Should().Be(string.Empty);
        }

        [Test]
        public void UpdateProfile_Result_True_Test()
        {
            ProfileRequest request = new ProfileRequest()
            {
                ProfileId = "1",
                OrganizationId = "1",
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
            profileService.CreateUpdateProfile(request).Should().Be("1");
        }

        [Test]
        public void UpdateProfile_Result_False_Test()
        {
            profileService.CreateUpdateProfile(null).Should().Be(string.Empty);
        }
    }
}