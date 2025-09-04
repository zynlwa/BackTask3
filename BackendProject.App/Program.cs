using BackendProject.App.Data;
using BackendProject.App.Models;
using BackendProject.App.Profiles;
using BackendProject.App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //                .AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //); 
            builder.Services.AddAutoMapper(opt => { }, typeof(MapperProfile).Assembly);
            builder.Services.AddDbContext<PustokDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddSession(opt=>
            opt.IdleTimeout=TimeSpan.FromMinutes(20)
                );
            
            builder.Services.AddIdentity<AppUser, IdentityRole>
                (options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<PustokDbContext>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
       );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
         

            app.Run();
        }
    }
}
