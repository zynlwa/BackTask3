using BackendProject.App.Data;
using BackendProject.App.Profiles;
using BackendProject.App.Services;
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

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
