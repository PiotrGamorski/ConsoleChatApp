using ConsoleChatApp.AppEventsProcessor;
using ConsoleChatApp.DependencyChanges;
using ConsoleChatApp.Entities;
using ConsoleChatApp.Interfaces;
using ConsoleChatApp.Repositories;
using ConsoleChatApp.Services;
using ConsoleChatApp.Services.EntityServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleChatApp
{
	static class Program
	{
		static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureServices((_, services) =>
				{
					services.AddSingleton<App>();
					services.AddSingleton<IUserRepository, UserRepository>();
					services.AddSingleton<IMessageRepository, MessageRepository>();
					services.AddSingleton<ITypeService, TypeService>();
					services.AddSingleton<IUserService, UserService>();
					services.AddSingleton<IMessageService<Message>, MessageService>();
					services.AddSingleton<ISqlNotificationService, SqlNotificationService>();
					services.AddSingleton<IProcessEvent, CurrentDomainProcessExit>();
					services.AddSingleton<UserDependencyChange>();
					services.AddSingleton<MessageDependencyChange>();
					services.AddSingleton<IProcessEvent, CurrentDomainProcessExit>();
				});
		}

		static async Task Main(string[] args)
		{
			using IHost host = CreateHostBuilder(args).Build();
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				await services.GetRequiredService<App>().Run(args);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
