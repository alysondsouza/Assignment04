using System;
using Assignment4.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace Assignment4.Entities.Tests
{
    public class UserRepositoryTests
    {
        private readonly KanbanContext _context;
        private readonly UserRepository _repository;
        public UserRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();

            context.users.AddRange(
                new User { Id = 1, Name = "User1", Email = "user1@gmail.com" },
                new User { Id = 2, Name = "User2", Email = "user2@gmail.com" },
                new User { Id = 3, Name = "User3", Email = "user3@gmail.com" },
                new User { Id = 4, Name = "User4", Email = "user4@gmail.com" },
                new User { Id = 5, Name = "User5", Email = "user5@gmail.com" },
                new User { Id = 6, Name = "User6", Email = "user6@gmail.com" }
            );

            context.SaveChanges();
            _context = context;
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void User_Create_returns_correct_tuple_given_UserCreateDTO()
        {
            var repository = new UserRepository(_context);

            var user = new UserCreateDTO()
            {
                Name = "Caspar",
                Id = 8,
                Email = "cafm@itu.dk"
            };
            var output = repository.Create(user);
            Assert.Equal((Response.Created, 8), output);
        }

        [Fact]
        public void User_ReadAll_Returns_the_entire_Repository_as_array()
        {

            var expected = new[] 
            {
                new UserDTO(1, "User1", "user1@gmail.com"), 
                new UserDTO(2, "User2", "user2@gmail.com"), 
                new UserDTO(3, "User3", "user3@gmail.com"), 
                new UserDTO(4, "User4", "user4@gmail.com"), 
                new UserDTO(5, "User5", "user5@gmail.com"), 
                new UserDTO(6, "User6", "user6@gmail.com")
            };

            var output = _repository.ReadAll();

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(output));
        }

        [Fact]
        public void User_Read_returns_correct_user_given_Id() 
        {
            var expected1 = new UserDTO(6, "User6", "user6@gmail.com");
            var output1 = _repository.Read(6);

            var expected2 = new UserDTO(3, "User3", "user3@gmail.com");
            var output2 = _repository.Read(3);

            var expected3 = new UserDTO(2, "User2", "user2@gmail.com");
            var output3 = _repository.Read(2);

            var expected4 = new UserDTO(5, "User5", "user5@gmail.com");
            var output4 = _repository.Read(5);
            Assert.Equal(expected1, output1);
            Assert.Equal(expected2, output2);
            Assert.Equal(expected3, output3);
            Assert.Equal(expected4, output4);
        }
    }
}
