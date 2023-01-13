using ConsoleChatApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleChatApp.Interfaces
{
    public interface IUserRepository
    {
        Person GetUserById(int id);
        Person GetUserByLogin(string login);
        Person GetOtherUserByLogin(string currentPersonLogin);
        bool IsOtherUserLoggedIn(string currentPersonLogin);
        Task<List<Person>> GetAllAvailableUsers();
    }
}
