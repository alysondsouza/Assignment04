using System.Reflection;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;
using System.Data.SqlClient;
using Assignment4;

namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {

        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = @"Server=localhost;Database=MyProject;User Id=sa;Password=37d6661e-6894-4944-88fc-e07256e30c81"; 
            //var connectionString = configuration.GetConnectionString("MyProject");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options); //See Class KanbanContext >> "options"
        }
        
        //       .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))

        
        public static void Seed(KanbanContext context)
        {
            var ali = new User {Name = "Ali", Email = "ades@itu.dk" };
            var mads = new User {Name = "Mads", Email = "mads@itu.dk" };
            var caspar = new User {Name = "Caspar", Email = "caspar@itu.dk" };

            var Csharp = new Tag {Name = "Csharp" };
            var Docker = new Tag {Name = "Docker" };
            var Java = new Tag {Name = "Java" };

            var task1 = new Task {Title = "Programs to Fix", AssignedTo =  ali, Description = "urgent", MyState = State.Active, Tags = new[]{Csharp, Java}};
            var task2 = new Task {Title = "Debbug", AssignedTo =  mads, Description = "manhana manhana", MyState = State.New, Tags = new[]{Docker}};
            
            context.Tasks.AddRange(
                task1, task2
            );
            context.SaveChanges();
        }
    }
}