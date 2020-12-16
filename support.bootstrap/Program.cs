using Microsoft.EntityFrameworkCore;
using Serilog;
using support_api.services.Data;
using System;
using System.Threading.Tasks;

namespace support.bootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("bootstrap.log")
                .CreateLogger();

            using (var dbContext = ConnectionHelper.CreateDbContext()) 
            {
                Log.Information("Generating db create script...");
                var createSQL = dbContext.Database.GenerateCreateScript();
                try
                {
                    dbContext.Database.EnsureCreated();
                    Log.Information("DB Script executed");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error when generating DB from script");
                }
            }
        }
    }
}
