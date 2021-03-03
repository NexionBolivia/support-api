using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Support.API.Services.Data;
using Support.API.Services.KoboData;
using System.IO;

namespace Support.API.Services.Helpers
{
    public static class ConnectionHelper
    {
        private const string _appSettingsFile = "appsettings.json";

        public static ApplicationDbContext CreateDbContext()
        {
            return CreateDbContext(GetConnectionString());
        }

        public static KoboDbContext CreateKoboDbContext()
        {
            return CreateKoboDbContext(GetConnectionString(_appSettingsFile, "KoboConnection"));
        }

        public static ApplicationDbContext CreateDbContext(string connectionString)
        {
            var connection = connectionString.ReplaceConnectionStringEnvVars();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connection);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public static KoboDbContext CreateKoboDbContext(string connectionString)
        {
            var connection = connectionString.ReplaceConnectionStringEnvVarsForKobo();

            var optionsBuilder = new DbContextOptionsBuilder<KoboDbContext>()
                .UseNpgsql(connection);

            return new KoboDbContext(optionsBuilder.Options);
        }

        public static string GetConnectionString(string? appSettingsFile = _appSettingsFile, string? connectionName = "SupportConnection")
        {
            var configuration = GetConfiguration(appSettingsFile);
            var connectionString = configuration.GetConnectionString(connectionName);
            return connectionString;
        }

        public static IConfiguration GetConfiguration(string appFile = _appSettingsFile)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // Duplicate here any configuration sources you use.
            configurationBuilder.AddJsonFile(appFile, optional: true, reloadOnChange: true)
                .SetBasePath(Directory.GetCurrentDirectory());
            return configurationBuilder.Build();
        }
    }
}
