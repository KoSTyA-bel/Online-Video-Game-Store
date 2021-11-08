using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    /// <summary>
    /// Provides a set of methods that should implement user services.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Checks the presence of the user in the database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Returns the user if there is one in the database, or null in all other cases.</returns>
        Task<User> GetUser(string login, string password);

        /// <summary>
        /// Checks if the login is busy.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>True if the login is busy and false if the login is not busy.</returns>
        Task<bool> ContainsUser(string login);

        /// <summary>
        /// Enters an entry about a user with administrator rights in the database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Task.</returns>
        Task RegistrAdmin(string login, string password);

        /// <summary>
        /// Enters an entry about a user with normal rights into the database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns></returns>
        Task RegistrUser(string login, string password);

        /// <summary>
        /// Tries to return the role by its ID.
        /// </summary>
        /// <param name="id">Role ID.</param>
        /// <returns>Returns the role if it exists, otherwise it returns null.</returns>
        ValueTask<Role> TryGetRole(int? id);
    }
}