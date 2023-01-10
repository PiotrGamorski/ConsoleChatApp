using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;

namespace ConsoleChatApp.Services.EntityServices
{
    public class MessageService : IMessageService<Message>
    {
        public void Add(Message message)
        {
            using (AppDbContext _dataContext = new AppDbContext())
            {
                _dataContext.Messages.Add(message);
                _dataContext.SaveChanges();
            }
        }
    }
}
