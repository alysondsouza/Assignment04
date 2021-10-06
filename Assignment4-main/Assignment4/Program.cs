using System.Reflection.Metadata;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;
using System;
using System.IO;
using System.Linq;
using Assignment4;

//DOCKER
//docker pull mcr.microsoft.com/mssql/server:2019-latest

//AZURE DATA STUDIO
//https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15

//CONNECTION STRING
//$connectionString
//$ConfirmPreference

//PASSWORD
//$password = New-Guid  (generate)
//$password (retrieve)
//$password.Guid | Set-Clipboard (set password to clipboard)

//RUN SQL SERVER CONTAINER: DOCKER
//$password = New-Guid  
//docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
//$database = "MyProject"
//$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"

//PACKEGE MANAGER
//www.nuget.org >> packages >> Search: SQL >> System.Data.SqlClient >> .NET CLI
//dotnet add package System.Data.SqlClient --version 4.8.3

//ENABLE USER SECRETS
//dotnet user-secrets init
//$ConfirmPreference
//$connectionString (get)
//$connectionString = "Server=localhost;Database=MyProject;User Id=sa;Password=3c311304-bb2e-461d-aa5e-0ed3c29894ea" (copy paste)
//dotnet user-secrets set "ConnectionStrings:MyProgram" "$connectionString"
//dotnet add package Microsoft.Extensions.Configuration.Json
//dotnet add package Microsoft.Extensions.Configuration.UserSecrets
//dotnet user-secrets list

//TO CREATE EF CORE
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite
//dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
//dotnet add package Microsoft.EntityFrameCore.Proxies
//dotnet add package Microsoft.EntityFrameworkCore.Design

//INSTALL THE ENTITY FRAMEWORK GLOBAL TOOL
//dotnet tool install --global dotnet-ef

//MIGRATION
//dotnet ef migrations add InitialCreate
// dotnet ef migrations add Initial -s .\file.csproj //output: Folder:Migration
//dotnet ef database update
// dotnet ef database update -s ..\folder\

//dotnet ef migrations add InitialMigration --project Assignment4.Entities --startup-project Assignment4
//dotnet ef update database --project Assignment4.Entities --startup-project Assignment4

namespace Assignment4
{
    class Program
    {
        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>();

            return builder.Build();
        }

        static void Main(string[] args)
        {

            var configuration = LoadConfiguration();
            var connectionString = "Server=localhost;Database=MyProject;User Id=sa;Password=a1f4d27d-3246-4252-beb7-936e3e9e15d9";//configuration.GetConnectionString("MyProgram");
            //var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseNpgsql(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlite(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);

            //using var connection = new SqlConnection(connectionString);

            //var connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"; //(retrieve password)
            //new KanbanContextFactory.Seed(Context);
            
            //connection.Open();
            /*QUERY
            var cmdTxt = "SELECT * FROM table WHERE Name = @name";
            using var command = new SqlCommand(cmdTxt, connection);
            using var reader = command.ExecuteReader();
            command.Parameters.AddWithValue("@name", name); //safer way to query
            while (reader.Read()){
                var character = new
                {
                    Name = reader.GetString("Name"),
                    Species = reader.GetString("Species")
                };
                Console.WriteLine(character);
            */
            //connection.Close();

        }

    }
}
