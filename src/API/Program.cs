using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

[assembly: InternalsVisibleTo("API.Tests")]

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            ProcessCommands(args, host);
            ProcessDbCommands(args, host);


            host.Run();
        }

        private static void ProcessCommands(string[] args, IWebHost host)
        {
            var scopeFactory = host.Services.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;
            using (var scope = scopeFactory.CreateScope())
            {
                if (args.Contains("createuser"))
                {
                    Console.WriteLine("Creating  user");
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var user = new IdentityUser { UserName = "admin@admin.com" };
                    userManager.CreateAsync(user, "Nothing123$").Wait();
                }
            }
        }

        private static void ProcessDbCommands(string[] args, IWebHost host)
        {
            var scopeFactory = host.Services.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;
            using (var scope = scopeFactory.CreateScope())
            {
                if (args.Contains("dropdb"))
                {
                    Console.WriteLine("Dropping database");
                    var db = GetDb(scope);
                    db.Database.EnsureDeleted();
                }

                if (args.Contains("migratedb"))
                {
                    Console.WriteLine("Migrating database");
                    var db = GetDb(scope);
                    db.Database.Migrate();
                }
            }

            if (args.Contains("stop"))
            {
                Console.WriteLine("Exiting on stop command");
                Environment.Exit(0);
            }
        }

        private static ApplicationDbContext GetDb(IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"api_log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
