using System.Threading.Tasks;

namespace ConsoleChatApp.Interfaces
{
    public interface ISqlNotificationService
    {
        Task CreateSqlNotification(IDependencyChange dependencyChange);
    }
}
