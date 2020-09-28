using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YourSoul4Christ.App.Shared.Entities;

namespace YourSoul4Christ.App.API
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Verse> Verses { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(configuration.GetConnectionString("AppDbConnectionString"));
    }
}
