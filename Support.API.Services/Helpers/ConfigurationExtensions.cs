using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Support.API.Services.Data;
using Support.API.Services.KoboData;

namespace Support.API.Services.Helpers
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureSupportDatabase(services, Configuration);
            ConfigureKoboDatabase(services, Configuration);
        }

        public static void ConfigureSupportDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(
                            Configuration.GetConnectionString("SupportConnection").ReplaceConnectionStringEnvVars()));
        }

        public static void ConfigureKoboDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<KoboDbContext>(
                    options => options.UseNpgsql(
                            Configuration.GetConnectionString("KoboConnection").ReplaceConnectionStringEnvVarsForKobo()));
        }
    }
}
