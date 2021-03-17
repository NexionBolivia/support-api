using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Support.API.Services.Helpers;
using Support.API.Services.KoboFormData;
using System;
using System.Linq;

namespace Support.API.Services.IntegrationTests
{
    public class KoboDbContextTest
    {
        internal IServiceProvider provider;
        private KoboFormDbContext dbContext;

        [OneTimeSetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("KOBO_DB_SERVER", "192.168.56.101");
            Environment.SetEnvironmentVariable("KOBO_DB_PORT", "5435");
            Environment.SetEnvironmentVariable("KOBO_DB_NAME", "koboform");
            Environment.SetEnvironmentVariable("KOBO_DB_USER", "kobo");
            Environment.SetEnvironmentVariable("KOBO_DB_PASSWORD", "{nNJ12.nY6ev");

            provider = ConfigureDIServices();
        }

        [Test]
        public void GetAllUsers_UsingKoboDbContext()
        {
            using (dbContext = provider.GetRequiredService<KoboFormDbContext>())
            {
                var users = dbContext.KoboUsers.ToList();

                users.Should().NotBeNullOrEmpty();

                var superAdminUser = users.FirstOrDefault(p => p.UserName == "super_admin");
                superAdminUser.Should().NotBeNull();
                superAdminUser.Id.Should().Be(1);
            }
        }

        private ServiceProvider ConfigureDIServices()
        {
            //setup our DI
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            var configuration = ConnectionHelper.GetConfiguration();

            serviceCollection.AddHttpContextAccessor(); // Used on DbContext
            serviceCollection.ConfigureKoboFormDatabase(configuration);
            serviceCollection.ConfigureKoboCatDatabase(configuration);

            return serviceCollection.BuildServiceProvider();
        }
    }
}