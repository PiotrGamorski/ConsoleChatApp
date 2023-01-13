using Microsoft.Data.SqlClient;
using System;
using ConsoleChatApp.Interfaces;
using System.Configuration;
using System.Threading.Tasks;
using ConsoleChatApp.DependencyChanges;

namespace ConsoleChatApp
{
	public class App
	{
		private readonly IProcessEvent _currentDomainProcessExit;
		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
		private readonly ISqlNotificationService _sqlNotificationService;
		private readonly IDependencyChange _userDependencyChange;
		private readonly IDependencyChange _messageDependencyChange;
		private readonly ITypeService _typeService;

		public App(
			IProcessEvent currentDomainProcessExit,
			IUserRepository userRepository,
			IUserService userService, 
			ISqlNotificationService sqlNotificationService, 
			UserDependencyChange userDependencyChange, 
			MessageDependencyChange messageDependencyChange,
			ITypeService typeService
			)
		{
			_currentDomainProcessExit = currentDomainProcessExit;
			_userRepository = userRepository;
			_userService = userService;
			_sqlNotificationService = sqlNotificationService;
			_userDependencyChange = userDependencyChange;
			_messageDependencyChange = messageDependencyChange;
			_typeService = typeService;
		}

		public async Task Run(string[] args)
		{ 
			OnInitialized();
			await OnCurrentUserLogin();
			await OnOtherUserLogin();
			_typeService.Type();
		}

		private void OnInitialized()
		{
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(_currentDomainProcessExit.Process);	
			SqlDependency.Start(ConfigurationManager.ConnectionStrings["Sample"].ConnectionString);
		}

		private async Task OnCurrentUserLogin()
		{
			Console.WriteLine("Enter your login:");
			CommonData.Login = Console.ReadLine();

			while (!_userService.IsLoginValid(CommonData.Login))
			{
				Console.WriteLine("Invalid login, please try again:");
				CommonData.Login = Console.ReadLine();
			}

			await _sqlNotificationService.CreateSqlNotification(_userDependencyChange);

			_userService.SetUserIsLogged(true, CommonData.Login);
			Console.WriteLine($"Success. Hello {CommonData.Login}!");
		}

		private async Task OnOtherUserLogin()
		{
			CommonData.IsOtherUserLoggedIn = _userRepository.GetOtherUserByLogin(CommonData.Login).IsLogged;
			await _sqlNotificationService.CreateSqlNotification(_messageDependencyChange);

			while (!CommonData.IsOtherUserLoggedIn)
			{
				Console.WriteLine("Waitnig for the other user... Press any key to check if user is logged in.");
				Console.ReadKey(true);
			}
		}
	}
}
