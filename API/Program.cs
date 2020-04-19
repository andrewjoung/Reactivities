using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Microsoft.EntityFrameworkCore;

// This is the start up application
// First that starts and runs 
namespace API
{
    public class Program
    {
        // Executes when we start application
        // Calls CreateHostBuilder
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // access datacontext
            // Only runs while Main is runing
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    // We get our data context from persistence that initalizes our two entities
                    // We then migrate them and actually add them to the database as tables
                    // Context referes to our database??? 
                    context.Database.Migrate();

                    // Here we grab seeds from persistence and call the SeedData method to populate the database
                    // It is passed in the context that we created 
                    // Inside SeedData it checks to see if there are any entries and if not it creates a new List and few new Activities
                    // And then calls the context.Activities (the table) .AddRange(list)
                    // And then context.SaveChanges
                    Seed.SeedData(context);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured during migration");
                }
            }

            host.Run();
        }

        // Returns IHostBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
