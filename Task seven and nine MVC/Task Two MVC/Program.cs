using Microsoft.EntityFrameworkCore;
using Task_Two_MVC.Data;

namespace Task_Two_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("sc"));
            });

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

            app.UseAuthorization();
            #region Custom route 

            // Route with optional category and id
            app.MapControllerRoute(
                name: "withCategory",
                pattern: "{controller}/{action}/{category?}/{id?}");

            // Route with only id
            app.MapControllerRoute(
                name: "withId",
                pattern: "{controller}/{action}/{id?}");
            #endregion

            #region Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
