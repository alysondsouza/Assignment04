using System;

//CMD: TO CREATE EF CORE
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite
//CMD: TO BIND DB POSTGRES
//dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL //--version 5.0.10



//dotnet add package Microsoft.EntityFrameworkCore

//dotnet tool install --global dotnet-ef
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet ef migrations add InitialCreate
//dotnet ef database update

//dotnet add package Microsoft.EntityFrameCore.Proxies


// INSTALL THE ENTITY FRA;EWORK GLOBAL TOOL
// dotnet tool install --global dotnet-ef

// RUN SQL SERVER CONTAINER
// $password = New-Guid
// docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
// $database = "Futurama"
// $connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"

// ENABLE USER SECRETS
// dotnet user-secrets init
// dotnet user-secrets set "ConnectionStrings:Futurama" "$connectionString"
// dotnet add package Microsoft.Extensions.Configuration.Json
// dotnet add package Microsoft.Extensions.Configuration.UserSecrets


namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
