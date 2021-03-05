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
                Log.Information("Generating db create script...");
                
                try
                {
                    dbContext.Database.EnsureCreated();
                    // Crear datos mínimos Support API
                    // Crear datos mínimos publicados por KPI 
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
