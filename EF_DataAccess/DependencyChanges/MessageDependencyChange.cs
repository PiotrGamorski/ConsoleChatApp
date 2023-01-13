using ConsoleChatApp.Interfaces;
using System;

namespace ConsoleChatApp.DependencyChanges
{
    public class MessageDependencyChange : IDependencyChange
    {
        public string TableName { get; }
        public string SqlCommand { get; }

        private readonly ISqlNotificationService _sqlNotification;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public MessageDependencyChange(ISqlNotificationService sqlNotification, IUserRepository personRepository, IMessageRepository messageRepository)
        {
            _sqlNotification = sqlNotification;
            _userRepository = personRepository;
            _messageRepository = messageRepository;
            TableName = "Messages";
            SqlCommand = $"SELECT [id], [Text], [MesssageRecipientId], [MesssageReciverId], [DateCreated] FROM dbo.{TableName}";
        }

        public async void OnDependencyChange(object sender, EventArgs e)
        {
            var currentUser = _userRepository.GetUserByLogin(CommonData.Login);

            string userName;

            if (_messageRepository.GetLastMessage().MesssageRecipientId == currentUser.id)
                userName = currentUser.FirstName;
            else
                userName = _userRepository.GetOtherUserByLogin(CommonData.Login).FirstName;

            Console.WriteLine($"{userName}: {_messageRepository.GetLastMessage().Text}");
            await _sqlNotification.CreateSqlNotification(this);
        }
    }
}
