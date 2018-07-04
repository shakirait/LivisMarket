using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Livis.Market.Autofac;
using Livis.Market.Data;
using Livis.Market.Infrastructure;
using Livis.Market.Utilities;
using Livis.Market.Utilities.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Livis.Market
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc();
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
            services.AddDbContext<LivisMarketContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString(Connection.ConnectionStringName), b => b.MigrationsAssembly(Connection.AssemblyMigrationName))
            );
            services.AddIdentity<LevisUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                                                   .AddEntityFrameworkStores<LivisMarketContext>()
                                                   .AddDefaultTokenProviders();
            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddSingleton(Configuration);
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options => {
                 options.LoginPath = "/login";
             });
            // Add this line:
            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");
            var containerBuilder = Bootstrapper.InitializeContainer(services);
            this.ApplicationContainer = Container.Instance;
            return new AutofacServiceProvider(this.ApplicationContainer);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("OrderComplete", "OrderCompletion",
                  new { controller = "Shopping", action = "Complete" });
                routes.MapRoute("MyConfirmation", "MyConfirmation",
                   new { controller = "Shopping", action = "Confirmation" });
                routes.MapRoute("MyPayment", "MyPayment",
                   new { controller = "Shopping", action = "Payment" });
                routes.MapRoute("MyInfo", "MyInfo",
                   new { controller = "Shopping", action = "Customer" });
                routes.MapRoute("MyCart", "MyCart",
                   new { controller = "Shopping", action = "Cart" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
