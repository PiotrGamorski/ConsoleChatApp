using ConsoleChatApp.AppEventsProcessor;
using ConsoleChatApp.DependencyChanges;
using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using ConsoleChatApp.Repositories;
using ConsoleChatApp.Services;
using ConsoleChatApp.Services.EntityServices;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ConsoleChatApp
{
    static class Program
    {    
         private static IUserRepository UserRepository;
         private static IMessageRepository MessageRepository;
         private static IUserService UserService;
         private static IMessageService<Message> MessageService;
         private static ISqlNotificationService SqlNotification;
         private static IProcessEvent CurrentDomainProcessExit;
         private static IDependencyChange UserDependancyChange;
         private static IDependencyChange MessageDependencyChange;
         private static ITypeService TypeService;

         static async Task Main(string[] args)
         {
            OnInitialized();
            await OnCurrentUserLogin();
            await OnOtherUserLogin();
            TypeService.Type();
         }

         private static void OnInitialized()
         {
            UserRepository = new UserRepository();
            MessageRepository = new MessageRepository();

            UserService = new UserService();
            MessageService = new MessageService();
            TypeService = new TypeService(UserRepository, MessageService);
            
            SqlNotification = new SqlNotificationService();
            UserDependancyChange = new UserDependencyChange(SqlNotification, UserRepository);
            MessageDependencyChange = new MessageDependencyChange(SqlNotification, UserRepository, MessageRepository);
            CurrentDomainProcessExit = new CurrentDomainProcessExit(UserService);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomainProcessExit.Process);

            SqlDependency.Start(ConfigurationManager.ConnectionStrings["Sample"].ConnectionString);
         }

         private static async Task OnCurrentUserLogin()
         {
            Console.WriteLine("Enter your login:");
            CommonData.Login = Console.ReadLine();

            while (!UserService.IsLoginValid(CommonData.Login))
            {
               Console.WriteLine("Invalid login, please try again:");
               CommonData.Login = Console.ReadLine();
            }

            await SqlNotification.CreateSqlNotification(UserDependancyChange);

            UserService.SetUserIsLogged(true, CommonData.Login);
            Console.WriteLine($"Success. Hello {CommonData.Login}!");
         }

         private static async Task OnOtherUserLogin()
         {
            CommonData.IsOtherUserLoggedIn = UserRepository.GetOtherUser(CommonData.Login).IsLogged;
            await SqlNotification.CreateSqlNotification(MessageDependencyChange);

            while (!CommonData.IsOtherUserLoggedIn)
            {
               Console.WriteLine("Waitnig for the other user... Press any key to check if user is logged in.");
               Console.ReadKey(true);
            }
         }
    }
}
