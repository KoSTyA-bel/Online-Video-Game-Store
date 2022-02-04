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
        /// Creates a new instance of the class <see cref="User"/>.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="User"/>
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
        /// User id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password.
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
        /// Id of user role.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// User role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="newPassword">New password.</param>
        public void ChangePassword(string newPassword) => _password = newPassword.GetHash();
    }
}
