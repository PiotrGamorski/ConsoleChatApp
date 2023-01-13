using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using System.Linq;

namespace ConsoleChatApp.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly AppDbContext _dbContext;

		public MessageRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Message GetLastMessage()
		{
			return _dbContext.Messages
						 .OrderBy(m => m.DateCreated)
						 .LastOrDefault();
		}
	}
}
