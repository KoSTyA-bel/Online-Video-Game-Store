using System.Threading.Tasks;

namespace GameStore.Models.Users
{
    public interface IUserService
    {
        Task<User> GetUser(string login, string password);
        Task<bool> ContainsUser(string login);
        Task RegistrAdmin(string login, string password);
        Task RegistrUser(string login, string password);
        ValueTask<Role> TryGetRole(int? id);
    }
}