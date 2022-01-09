using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    /// <summary>
    /// Container class that stores information about the role.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Create a new instanse of <see cref="Role"/>.
        /// </summary>
        public Role()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Role Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Role name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of users with this role.
        /// </summary>
        public List<User> Users { get; set; }
    }
}
