using ESKOBApi.Authentication;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace ESKOBApi
{
    public class ESKOBDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NFUOR0K;Initial Catalog=Test;Integrated Security=True;MultipleActiveResultSets=true")
            .UseLazyLoadingProxies();

            optionsBuilder.ConfigureWarnings(warnings => warnings
            .Log(CoreEventId.LazyLoadOnDisposedContextWarning));
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Added_User> Added_Users { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
