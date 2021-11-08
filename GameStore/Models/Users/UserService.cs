using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    /// <summary>
    /// A specific user service that implements the interface <see cref="IUserService"/>.
    /// </summary>
    public class UserService : IUserService
    {
        private UserContext _context;

        /// <summary>
        /// Initializes a new class object <see cref="UserService"/>.
        /// </summary>
        /// <param name="context">Database.</param>
        public UserService(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task RegistrUser(string login, string password)
        {
            var user = new User() { Login = login, Password = password};
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

            if (userRole != null)
            {
                user.Role = userRole;

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task RegistrAdmin(string login, string password)
        {
            var user = new User() { Login = login, Password = password };
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "admin");

            if (userRole != null)
            {
                user.Role = userRole;
                user.RoleId = userRole.Id;

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public Task<User> GetUser(string login) => _context.Users.FirstOrDefaultAsync(user => user.Login == login);

        /// <inheritdoc/>
        public Task<User> GetUser(string login, string password) => _context.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password);

        /// <inheritdoc/>
        public Task<bool> ContainsUser(string login) => _context.Users.Select(x => x.Login).ContainsAsync(login);

        /// <inheritdoc/>
        public ValueTask<Role> TryGetRole(int? id) => _context.Roles.FindAsync(id);   
    }
}
