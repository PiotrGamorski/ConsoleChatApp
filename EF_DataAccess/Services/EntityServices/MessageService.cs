using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;

namespace ConsoleChatApp.Services.EntityServices
{
	public class MessageService : IMessageService<Message>
	{
		private readonly AppDbContext _dbContext;

		public MessageService(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Add(Message message)
		{
			_dbContext.Messages.Add(message);
			_dbContext.SaveChanges();
		}
	}
}
