using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using System.Linq;

namespace ConsoleChatApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public Message GetLastMessage()
        {
            using (AppDbContext _dataContext = new AppDbContext())
            {
                return _dataContext.Messages
                    .OrderBy(m => m.DateCreated)
                    .LastOrDefault();
            }
        }
    }
}
