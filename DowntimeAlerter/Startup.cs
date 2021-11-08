using DowntimeAlerter.Business.Abstract;
using DowntimeAlerter.Business.Concrete;
using DowntimeAlerter.Business.Notification;
using DowntimeAlerter.Data;
using DowntimeAlerter.DataAccess.Abstract;
using DowntimeAlerter.DataAccess.Concrete;
using DowntimeAlerter.DataAccess.Repository;
using DowntimeAlerter.Domain.Entities;
using DowntimeAlerter.MVC.UI.Filters;
using DowntimeAlerter.MVC.UI.Mapper;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ITargetAppDal, TargetAppDal>();
            services.AddScoped<ITargetAppService, TargetAppManager>();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IHealthCheckDal, HealthCheckDal>();
            services.AddScoped<IHealthCheckService, HealthCheckManager>();
            services.AddScoped<IHangfireJobService, HangfireJobManager>();
            services.AddScoped<INotificationSender, MailNotificationSender>();
            services.AddScoped<ILogDal, LogDal>();
            services.AddScoped<ILogService, LogManager>();
            services.Configure<MailSettings>(Configuration.GetSection("MailAccountSettings"));

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.Zero,
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
            }));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHangfireDashboard(
                pathMatch: "/hangfiredashboard",
                options: new DashboardOptions()
                {
                    Authorization = new IDashboardAuthorizationFilter[] {
                        new HangfireAuthorizationFilter()
                    }
                });

            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
