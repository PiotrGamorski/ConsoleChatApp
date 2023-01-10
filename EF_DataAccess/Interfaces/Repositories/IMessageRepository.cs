using ConsoleChatApp.Entities;

namespace ConsoleChatApp.Interfaces
{
    public interface IMessageRepository
    {
        Message GetLastMessage();
    }
}
