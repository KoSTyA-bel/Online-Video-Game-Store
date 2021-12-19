using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    /// <summary>
    /// Class-container of user information.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Id of user role.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// User role.
        /// </summary>
        public Role Role { get; set; }
    }
}
