using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using support_api.services.Data;
using System.IO;

namespace support.bootstrap
{
    public static class ConnectionHelper
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var connectionString = GetConnectionString().ReplaceConnectionStringEnvVars();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public static string GetConnectionString(string? appSettingsFile = "appSettings.json", string? connectionName = "defaultConnection")
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appSettingsFile, optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString(connectionName);
            return connectionString;
        }
    }
}
