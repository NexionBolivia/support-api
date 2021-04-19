using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Support.API.Services.Data;
using Support.API.Services.KoboCatData;
using Support.API.Services.KoboFormData;

namespace Support.API.Services.Helpers
{
    public static class ConfigurationExtensions
    {
        private const string cSupportConnectionName = "SupportConnection";
        private const string cKoboFormConnectionName = "KoboFormConnection";
        private const string cKoboCatConnectionName = "KoboCatConnection";

        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureSupportDatabase(services, Configuration);
            ConfigureKoboFormDatabase(services, Configuration);
            ConfigureKoboCatDatabase(services, Configuration);
        }

        public static void ConfigureSupportDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                    options =>
                        options.UseNpgsql(
                            Configuration.GetConnectionString(cSupportConnectionName).ReplaceConnectionStringEnvVars())
                    );
        }

        public static void ConfigureKoboFormDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<KoboFormDbContext>(
                    options => options.UseNpgsql(
                            Configuration.GetConnectionString(cKoboFormConnectionName).ReplaceConnectionStringEnvVarsForKoboForm()));
        }

        public static void ConfigureKoboCatDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<KoboCatDbContext>(
                    options => options.UseNpgsql(
                            Configuration.GetConnectionString(cKoboCatConnectionName).ReplaceConnectionStringEnvVarsForKoboCat()));
        }
    }
}
