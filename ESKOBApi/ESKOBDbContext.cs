using ESKOBApi.Models;
using ESKOBApi.Models.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ESKOBApi
{
    public class ESKOBDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VIAUEKC;Initial Catalog=Test;Integrated Security=True;MultipleActiveResultSets=true")
            .UseLazyLoadingProxies();

            optionsBuilder.ConfigureWarnings(warnings => warnings
            .Log(CoreEventId.LazyLoadOnDisposedContextWarning));
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
