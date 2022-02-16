namespace GameStore.Services.Users
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
        /// <returns>Returns the user if there is one in the database, or null in all other cases.</returns>
        User GetUser(string login);

        /// <summary>
        /// Checks if the login is busy.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>True if the login is busy and false if the login is not busy.</returns>
        bool ContainsUser(string login);

        /// <summary>
        /// Enters an entry about a user with administrator rights in the database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Task.</returns>
        int RegistrAdmin(string login, string password);

        /// <summary>
        /// Enters an entry about a user with normal rights into the database.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Id of registered user.</returns>
        int RegistrUser(string login, string password);

        /// <summary>
        /// Tries to return the role by its ID.
        /// </summary>
        /// <param name="id">Role ID.</param>
        /// <returns>Returns the role if it exists, otherwise it returns null.</returns>
        Role TryGetRole(int? id);
    }
}