using System;

namespace GameStore.Services.Users
{
    /// <summary>
    /// A specific user service that implements the interface <see cref="IUserService"/>.
    /// </summary>
    public class UserService : IUserService
    {
        private IUserContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">Database.</param>
        public UserService(IUserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public int RegistrUser(string login, string password)
        {
            var user = new User(login, password);
            var userRole = _context.SelectRole(2);

            if (userRole != null)
            {
                user.Role = userRole;
                user.RoleId = userRole.Id;

                _context.AddUser(user);
            }

            return -1;
        }

        /// <inheritdoc/>
        public int RegistrAdmin(string login, string password)
        {
            var user = new User(login, password);
            var userRole = _context.SelectRole(1);

            if (userRole != null)
            {
                user.Role = userRole;
                user.RoleId = userRole.Id;

                return _context.AddUser(user);
            }

            return -1;
        }

        /// <inheritdoc/>
        public User GetUser(int id) => _context.SelectUser(id);

        /// <inheritdoc/>
        public User GetUser(string login) => _context.SelectUser(login);

        /// <inheritdoc/>
        public bool ContainsUser(string login) => _context.SelectUser(login) != null;

        /// <inheritdoc/>
        public Role TryGetRole(int? id) => _context.SelectRole(id.Value);
    }
}
