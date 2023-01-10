using ConsoleChatApp.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace ConsoleChatApp.Services
{
    public class SqlNotificationService : ISqlNotificationService
    {
        public async Task CreateSqlNotification(IDependencyChange dependencyChange)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sample"].ConnectionString))
            {
                await sqlConnection.OpenAsync();
                string sqlCommandText = dependencyChange.SqlCommand;

                if (sqlCommandText.Contains(dependencyChange.TableName))
                {
                    using (SqlCommand command = new SqlCommand(sqlCommandText, sqlConnection))
                    {
                        SqlDependency dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependencyChange.OnDependencyChange);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) { }
                    }
                }
            }
        }
    }
}
