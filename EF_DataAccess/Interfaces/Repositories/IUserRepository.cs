using ConsoleChatApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleChatApp.Interfaces
{
    public interface IUserRepository
    {
        Person GetUser(int id);
        Person GetUser(string login);
        Person GetOtherUser(string currentPersonLogin);
        bool IsOtherUserLoggedIn(string currentPersonLogin);
        Task<List<Person>> GetAvailableUsers();
    }
}
