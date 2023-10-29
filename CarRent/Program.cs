using CarRent.Database;
using CarRent.Services.Abstracts;
using Meridian_Web.Services.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CarRent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddMvc();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("KamilPC"));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "Identity";
                o.ExpireTimeSpan= TimeSpan.FromMinutes(5);
                o.LoginPath = "/auth/login";
                o.AccessDeniedPath = "/admin/product/list";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
               name: "default",
               pattern: "{area=exists}/{controller=home}/{action=index}");

            app.MapRazorPages();

            app.Run();
        }
    }
}