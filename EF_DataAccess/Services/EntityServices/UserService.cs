using ConsoleChatApp.Interfaces;
using System.Linq;

namespace ConsoleChatApp.Services.EntityServices
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _dbContext;

		public UserService(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public bool IsLoginValid(string login)
		{
			return !string.IsNullOrEmpty(
					 _dbContext.People.FirstOrDefault(p => p.EmailAddress == login)?.EmailAddress);
		}

		public void SetUserIsLogged(bool isLogged, string login)
		{
			_dbContext.People.FirstOrDefault(p => p.EmailAddress == login).IsLogged = isLogged;
			_dbContext.SaveChanges();
		}
	}
}
