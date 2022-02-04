using System.Collections.Generic;

namespace GameStore.Services.Users
{
    /// <summary>
    /// It represents methods that must be implemented in classes that represent working with users database.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Adds user data into DB.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Id of crateted data.</returns>
        int AddUser(User user);
        
        /// <summary>
        /// Remove user from DB.
        /// </summary>
        /// <param name="id">User id to delete.</param>
        void DeleteUser(int id);

        /// <summary>
        /// Select all users that contains in DB.
        /// </summary>
        /// <returns>List of all users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Selects count of users in DB.
        /// </summary>
        /// <returns>Count of users.</returns>
        int GetCountOfUsers();

        /// <summary>
        /// Selects user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>If a user with this id exists, then the user, otherwise null.</returns>
        User SelectUser(int id);

        /// <summary>
        /// Selects user by login.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>If a user with this id exists, then the user, otherwise null.</returns>
        User SelectUser(string login);

        /// <summary>
        /// Update user data.
        /// </summary>
        /// <param name="user">The user whose data needs to be updated</param>
        void UpdateUser(User user);

        /// <summary>
        /// Selects role.
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>If role with this id exists, then the role, otherwise null.</returns>
        Role SelectRole(int id);
    }
}