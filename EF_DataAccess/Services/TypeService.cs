using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using System;

namespace ConsoleChatApp.Services
{
    public class TypeService : ITypeService
    {
        private readonly IUserRepository _repository;
        private readonly IMessageService<Message> _service;

        public TypeService(IUserRepository repository, IMessageService<Message> service)
        {
            _repository = repository;
            _service = service;
        }

        public void Type()
        {
            var otherUser = _repository.GetOtherUserByLogin(CommonData.Login);
            Console.WriteLine($"{otherUser.FirstName} {otherUser.LastName} is already logged in. Type your message:");
            var hasOtherUserLoggedOut = false;

            while (true)
            {
                if (CommonData.IsOtherUserLoggedIn)
                {
                    if (hasOtherUserLoggedOut)
                    {
                        hasOtherUserLoggedOut = false;
                        Console.WriteLine($"{otherUser.FirstName} {otherUser.LastName} is logged in again. Type your message:");
                    }

                    string messageText = Console.ReadLine();
                    ClearLastLine();

                    using (AppDbContext _dataContext = new AppDbContext())
                    {
                        var currentUserId = _repository.GetUserByLogin(CommonData.Login).id;
                        var messageRecieverId = _repository.GetOtherUserByLogin(CommonData.Login).id;

                        var message = Message.Factory.Create(messageText, currentUserId, messageRecieverId);
                        _service.Add(message);
                    }
                }
                else
                {
                     hasOtherUserLoggedOut = true;
                     Console.WriteLine($"{otherUser.FirstName} {otherUser.LastName} has logged out... Press any key to check if user is logged in.");
                     Console.ReadKey(true);
                }
            }
        }

        private void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
