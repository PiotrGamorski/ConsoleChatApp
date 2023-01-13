using ConsoleChatApp.Interfaces;
using System;

namespace ConsoleChatApp.DependencyChanges
{
    public class UserDependencyChange : IDependencyChange
    {
        public  string TableName { get; }
        public string SqlCommand { get; }

        private readonly ISqlNotificationService _sqlNotificationService;
        private readonly IUserRepository _repository;
        
        public UserDependencyChange(ISqlNotificationService sqlNotificationService, IUserRepository repository)
        {
            _sqlNotificationService = sqlNotificationService;
            _repository = repository;
            TableName = "People";
            SqlCommand = $"SELECT[id], [FirstName], [LastName], [EmailAddress], [PhoneNumber], [IsLogged] FROM dbo.{TableName}";
        }

        public async void OnDependencyChange(object sender, EventArgs e)
        {
            using (AppDbContext _dataContex = new AppDbContext())
            {
                CommonData.IsOtherUserLoggedIn = _repository.GetOtherUserByLogin(CommonData.Login).IsLogged;
                await _sqlNotificationService.CreateSqlNotification(this);
            }
        }
    }
}
