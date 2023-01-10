using ConsoleChatApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ConsoleChatApp
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
         
            // use this one for migrations
            //optionsBuilder.UseSqlServer("Server=PGAMORSKI\\SQL2016;Database=Sample;Trusted_Connection=True;");
        }
    }
}
