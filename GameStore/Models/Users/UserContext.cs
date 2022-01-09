using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    public sealed class UserContext : DbContext, IUserContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public int AddUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            this.Users.Add(user);
            SaveChanges();
            return user.Id;
        }

        public void DeleteUser(int id)
        {
            var user = SelectUser(id);

            if (user != null)
            {
                this.Users.Remove(user);
                SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers() => this.Users;

        public int GetCountOfUsers() => this.Users.Count();

        public Role SelectRole(int id) => this.Roles.Where(role => role.Id == id).FirstOrDefault();

        public User SelectUser(int id) => this.Users.Where(user => user.Id == id).FirstOrDefault();

        public User SelectUser(string login)
        {
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            return this.Users.Where(user => login == user.Login).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            this.Users.Update(user);
            SaveChanges();
        }

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
