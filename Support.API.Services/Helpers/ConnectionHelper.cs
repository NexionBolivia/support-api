using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Support.API.Services.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Support.API.Services.Helpers
{
    public static class ConnectionHelper
    {
        public static ApplicationDbContext CreateDbContext()
        {
            return CreateDbContext(GetConnectionString());
        }

        public static ApplicationDbContext CreateDbContext(string connectionString)
        {
            var connection = connectionString.ReplaceConnectionStringEnvVars();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connection);

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
