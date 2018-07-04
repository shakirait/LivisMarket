﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Livis.Market.Data;
using Livis.Market.Data.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Livis.Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host  = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    using (var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var context = services.GetRequiredService<LivisMarketContext>();
                        DbInitializer.Initialize(context);
                        var userManager = services.GetRequiredService<UserManager<LevisUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        SeedData.Initialize(context, userManager, roleManager).Wait();

                    }

                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
