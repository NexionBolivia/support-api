using Microsoft.EntityFrameworkCore;
using Serilog;
using Support.API.Services.Helpers;
using System;

namespace Support.Bootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("bootstrap.log")
                .WriteTo.Console()
                .CreateLogger();

            using (var dbContext = ConnectionHelper.CreateDbContext()) 
            {
                Log.Information("Generating db create script for Application Data...");
                
                try
                {
                    dbContext.Database.EnsureCreated();
                    // Crear datos mínimos Support API
                    Log.Information("DB Script executed for Application Data");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error when generating DB from script for Application Data");
                }
            }

            //Uncomment the following to enable bootstrap local KoboDbContext
            /*using (var dbContext = ConnectionHelper.CreateKoboDbContext())
            {
                Log.Information("Generating db create script for Kobo Data...");

                try
                {
                    dbContext.Database.EnsureCreated();
                    // Crear datos mínimos publicados por KPI 
                    Log.Information("DB Script executed for Kobo Data");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error when generating DB from script for Kobo Data");
                }
            }*/
        }
    }
}