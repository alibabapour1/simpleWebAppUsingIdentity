using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using simpleWebAppUsingIdentity.Models.DbContext;
using simpleWebAppUsingIdentity.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddDbContext<MyDbContext>(p => p.UseSqlServer("server=.;Database=TestingIdentity;Trusted_connection=True;MultipleActiveResultsets=True;Integrated Security=true;TrustServerCertificate=True"));
        builder.Services.AddControllersWithViews();
        builder.Services.AddIdentity<User, Roles>()
            .AddEntityFrameworkStores<MyDbContext>()
            .AddDefaultTokenProviders()
            ;
        


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}