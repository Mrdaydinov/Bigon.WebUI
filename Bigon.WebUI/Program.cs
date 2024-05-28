using Bigon.WebUI.Helpers;
using Bigon.WebUI.Helpers.Services;
using Bigon.WebUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bigon.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSCString"));
            });

            builder.Services.Configure<EmailOptions>(cfg =>
            {
                builder.Configuration.GetSection("emailAccount").Bind(cfg);
            });

            builder.Services.AddSingleton<IEmailService, EmailService>();

            var app = builder.Build();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            
            app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
                
            app.UseStaticFiles();

            app.Run();
        }
    }
}
