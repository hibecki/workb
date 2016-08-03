using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PPcore.Models;
using PPcore.Services;
using System.Collections.Generic;
using System.Globalization;

namespace PPcore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            //var connection = @"Server=(localdb)\ProjectsV13;Database=PalangPanyaDB;Trusted_Connection=True;";
            //services.AddDbContext<PalangPanyaDBContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<PalangPanyaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(i => {
                i.User.RequireUniqueEmail = false;
                i.Password.RequireDigit = false;
                i.Password.RequireLowercase = false;
                i.Password.RequireUppercase = false;
                i.Password.RequireNonAlphanumeric = false; ;
                i.Password.RequiredLength = 1;
                i.SignIn.RequireConfirmedEmail = false;
                i.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton(Configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorRights", policy => policy.RequireRole("Administrators"));
                options.AddPolicy("OperationRights", policy => policy.RequireRole("Administrators", "Operators"));
            });

            // Add framework services.
            services.AddMvc();

            services.AddTransient<IEmailSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            /**
            var supportedCultures = new[]
            {
                new CultureInfo("th-TH"),
            };

            var ci = new CultureInfo("th-TH");
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            ci.DateTimeFormat.LongDatePattern = "dd MMMM yyyy";

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            **/

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
