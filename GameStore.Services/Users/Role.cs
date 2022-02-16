using System.Collections.Generic;

namespace GameStore.Services.Users
{
    /// <summary>
    /// Container class that stores information about the role.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Gets or sets role Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets role name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets list of users with this role.
        /// </summary>
        public List<User> Users { get; set; }
    }
}
