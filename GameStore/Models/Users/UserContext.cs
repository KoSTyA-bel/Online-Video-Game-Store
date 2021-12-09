using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    public sealed class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            var admin = new Role() { Id = 1, Name = "admin" };
            var user = new Role() { Id = 2, Name = "user" };

            modelBuilder.Entity<Role>().HasData(new Role[] { admin, user });

            base.OnModelCreating(modelBuilder);
        }
    }
}
