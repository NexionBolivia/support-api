using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Support.API.Services.Helpers;
using Support.API.Services.Services;

namespace Support.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabases(Configuration);
            services.ConfigureAuthentication(Configuration);
            services.ConfigureCors();
                       
            services.AddControllers();
            
            services.AddScoped<IProfileService, ProfileService>(); // register service
            services.AddScoped<IKoboUserService, KoboUserService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerAuthorizationOptions("Support API", "API services for support");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Support API"); });
            }
            
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
