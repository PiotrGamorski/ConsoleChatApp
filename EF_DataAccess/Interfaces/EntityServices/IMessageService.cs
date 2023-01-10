using System;

namespace ConsoleChatApp.Interfaces
{
    public interface IMessageService<Message>
    {
        void Add(Message message);
    }
}
