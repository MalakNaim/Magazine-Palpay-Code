using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web.Extensions;
using Magazine_Palpay.Web.IdentityModels;
using Magazine_Palpay.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
var extensionsPath = environment.ContentRootPath + _config["Extensions:Path"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseOracle(_config.GetConnectionString("oracle"),
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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

}); 
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); 
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>(c =>
{
    c.BaseAddress = new Uri("https://api.exchangerate.host/");
});
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 30;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomLeft;
});
builder.Services.AddSingleton<GlobalExceptionHandler>();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 30;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomLeft;
});

var app = builder.Build();
app.UseNotyf();
if (environment.IsDevelopment())
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
app.Run();