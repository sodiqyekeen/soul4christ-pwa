using Microsoft.EntityFrameworkCore;
using YourSoul4Christ.App.Entities;

namespace YourSoul4Christ.App.API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Verse> Verses { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }
    }
}
