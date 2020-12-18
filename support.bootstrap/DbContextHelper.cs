using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Support.API.Services.Data;
using System.IO;

namespace Support.Bootstrap
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

        public static string GetConnectionString(string? appSettingsFile = "appsettings.json", string? connectionName = "defaultConnection")
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
