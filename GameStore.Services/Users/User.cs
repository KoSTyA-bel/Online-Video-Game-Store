using System;
using System.Security.Cryptography;
using System.Text;

namespace GameStore.Services.Users
{
    /// <summary>
    /// Class-container of user information.
    /// </summary>
    public class User
    {
        private byte[] _password = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <param name="roleId">Role id.</param>
        /// <param name="role">User role.</param>
        public User(string login, string password, int? roleId = null, Role role = null)
        {
            Login = login;
            this.ChangePassword(password);
            RoleId = roleId;
            Role = role;
        }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets user password.
        /// </summary>
        public byte[] Password 
        { 
            get 
            {
                var result = new byte[_password.Length];
                Array.Copy(_password, result, result.Length);
                return result;
            }

            set => _password = value;
        }

        /// <summary>
        /// Gets or sets id of user role.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets user role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="newPassword">New password.</param>
        public void ChangePassword(string newPassword) => _password = newPassword.GetMD5Hash();
    }
}
