using ConsoleChatApp.Interfaces;
using System.Linq;

namespace ConsoleChatApp.Services.EntityServices
{
    public class UserService : IUserService
    {
        public bool IsLoginValid(string login)
        {
            using (AppDbContext _dataContext = new AppDbContext())
            {
                return !string.IsNullOrEmpty(
                    _dataContext.People.FirstOrDefault(p => p.EmailAddress == login)?.EmailAddress);
            }
        }

        public void SetUserIsLogged(bool isLogged, string login)
        {
            using (AppDbContext _dataContext = new AppDbContext())
            {
                _dataContext.People.FirstOrDefault(p => p.EmailAddress == login).IsLogged = isLogged;
                _dataContext.SaveChanges();
            }
        }
    }
}
