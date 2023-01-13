using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleChatApp.Repositories
{
	// For simplicty, we assume that there are only two
	// users with unique emails/logins
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _dbContext;

		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Person GetUserByLogin(string login)
		{
			return _dbContext.People.FirstOrDefault(p => p.EmailAddress == login);
		}

		public Person GetOtherUserByLogin(string currentPersonLogin)
		{
			return _dbContext.People.FirstOrDefault(p => p.EmailAddress != currentPersonLogin);
		}

		// (Example) Getting response from DB without DbContext
		public Person GetUserById(int id)
		{
			using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sample"].ConnectionString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.People WHERE id = {id}", sqlConnection))
				{
					var sqlReader = sqlCommand.ExecuteReader();

					var response = new Person();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							response.id = sqlReader
								 .GetInt32(sqlReader.GetOrdinal("id"));
							response.FirstName = sqlReader
								 .GetString(sqlReader.GetOrdinal("FirstName"));
							response.LastName = sqlReader
								 .GetString(sqlReader.GetOrdinal("LastName"));
							response.EmailAddress = sqlReader
								 .GetString(sqlReader.GetOrdinal("EmailAddress"));
							response.PhoneNumber = sqlReader
								 .GetString(sqlReader.GetOrdinal("PhoneNumber"));
						}
					}

					return response;
				}
			}
		}

		public async Task<List<Person>> GetAllAvailableUsers()
		{
			return await _dbContext.People.Where(p => p.IsLogged).ToListAsync();
		}

		public bool IsOtherUserLoggedIn(string currentPersonLogin)
		{
			return GetOtherUserByLogin(currentPersonLogin).IsLogged;
		}
	}
}
