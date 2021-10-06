using System.Reflection;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;
using System.Data.SqlClient;
using Assignment4;

//.AddUserSecrets<Program>()

namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {

        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = "Server=localhost;Database=MyProject;User Id=sa;Password=a1f4d27d-3246-4252-beb7-936e3e9e15d9";

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options); //See Class KanbanContext >> "options"
        }
        
        //        .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
        //                 .AddUserSecrets<Program>()

        
        public static void Seed(KanbanContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE dbo.Users");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tags");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tasks");
            context.Database.ExecuteSqlRaw("DELETE dbo.TagTask");
            context.Database.ExecuteSqlRaw("DELETE dbo.User");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tag");
            context.Database.ExecuteSqlRaw("DELETE dbo.Task");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)");

            var username = new User { Id = 1, Name = "Ali", Email = "ades@itu.dk" };

            var Tag = new Tag { Id = 1, Name = "tagName" };

            var Task = new Task { Id = 1, Title = "tasktitle", AssignedTo =  new User { Id = 2, Name = "Mads", Email = "mads@itu.dk" }, Description = "descriptionTask", MyState = State.Active};
            
            
            context.TagTask.AddRange(
            new Task { Id = 1, Title = "tasktitle", AssignedTo =  new User { Id = 2, Name = "Mads", Email = "mads@itu.dk" }, Description = "descriptionTask", MyState = State.Active}               //new Character { GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = DateTime.Parse("1938-04-18"), Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
            );
            
            context.SaveChanges();
        }
    }

}