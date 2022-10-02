using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web.Extensions;
using Magazine_Palpay.Web.IdentityModels;
using Magazine_Palpay.Web.Persistence;
using Magazine_Palpay.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Magazine_Palpay
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
            options.UseOracle(Configuration.GetConnectionString("oracle"),
            e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            .UseOracleSQLCompatibility("11"))).AddIdentity<FluentUser, FluentRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

            });
            //services
            //.AddDatabaseContext<ApplicationDbContext>(GetIPAddress());
            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>(c =>
            {
                c.BaseAddress = new Uri("https://api.exchangerate.host/");
            });
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 30;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomLeft;
            });
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 30;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomLeft;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseNotyf();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseEndpoints(endpoints =>
            {
              //  endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                 // name: "areas",
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}");
                  //pattern: "{area:Admin}/{controller=Home}/{action=Index}/{id?}"
               // );
            });
        }
        private string GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())
           .FirstOrDefault(ha => ha.AddressFamily == AddressFamily.InterNetwork)
           .ToString();
        }
    }
}
