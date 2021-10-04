using System;

//TO CREATE EF CORE
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite
//dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
//dotnet add package Microsoft.EntityFrameCore.Proxies

//MIGRATION
//dotnet tool install --global dotnet-ef
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet ef migrations add InitialCreate
//dotnet ef database update

//INSTALL THE ENTITY FRA;EWORK GLOBAL TOOL
//dotnet tool install --global dotnet-ef

//FIND PASSWORD
//$password = New-Guid
//$password

//RUN SQL SERVER CONTAINER
//$password = New-Guid
//docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
//$database = "MyProject"
//$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"

//ENABLE USER SECRETS
//dotnet user-secrets init
//dotnet user-secrets set "ConnectionStrings:Futurama" "$connectionString"
//dotnet add package Microsoft.Extensions.Configuration.Json
//dotnet add package Microsoft.Extensions.Configuration.UserSecrets

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            using var context = new KanbanContext(optionsBuilder.Options);        }
    }
}
