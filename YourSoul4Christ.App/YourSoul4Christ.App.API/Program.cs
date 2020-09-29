using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace YourSoul4Christ.App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    SeedData.Initialize(db);
            //    //if (db.Database.EnsureCreated())
            //    //{
            //    //}
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
