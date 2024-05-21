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
                
            var app = builder.Build();

            app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
                
            app.UseStaticFiles();

            app.Run();
        }
    }
}
