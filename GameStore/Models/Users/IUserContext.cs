using System.Collections.Generic;

namespace GameStore.Models.Users
{
    public interface IUserContext
    {
        int AddUser(User user);

        void DeleteUser(int id);

        IEnumerable<User> GetAllUsers();

        int GetCountOfUsers();

        User SelectUser(int id);

        User SelectUser(string login);

        void UpdateUser(User user);

        Role SelectRole(int id);
    }
}