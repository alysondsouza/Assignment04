using System.Reflection;
using System;
using Assignment4.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;
using Assignment4;
using Assignment4.Entities;

namespace Assignment4.Entities.Tests
{
    public class TagRepositoryTests
    {
        private readonly KanbanContext _context;
        private readonly TagRepository _repository;
        public TagRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();

            context.tags.AddRange(
                new Tag { Id = 1, Name = "Tag number 1" },
                new Tag { Id = 2, Name = "Tag number 2" },
                new Tag { Id = 3, Name = "Tag number 3" },
                new Tag { Id = 4, Name = "Tag number 4" },
                new Tag { Id = 5, Name = "Tag number 5" },
                new Tag { Id = 6, Name = "Tag number 6" },
                new Tag { Id = 7, Name = "Tag number 7" }
            );

            context.SaveChanges();
            _context = context;
            _repository = new TagRepository(_context);

        }

        [Fact]
        public void Tag_Create_returns_tuple_of_CreatedResponse_and_8()
        {
            var repository = new TagRepository(_context);
            var myDTO = new TagCreateDTO() { Id = 8, Name = "This is a new tag" };
            var output = repository.Create(myDTO);
            Assert.Equal((Response.Created, 8), output);
        }

        [Fact]
        public void Tag_ReadAll_Returns_entire_repository()
        {
            var expected = new[]
            {
                new TagDTO(1, "Tag number 1"),
                new TagDTO(2, "Tag number 2"),
                new TagDTO(3, "Tag number 3"),
                new TagDTO(4, "Tag number 4"),
                new TagDTO(5, "Tag number 5"),
                new TagDTO(6, "Tag number 6"),
                new TagDTO(7, "Tag number 7"),
            };
            var output = JsonConvert.SerializeObject(_repository.ReadAll());
            Assert.Equal(JsonConvert.SerializeObject(expected), output);
        }

        [Fact]
        public void Tag_Read_returns_TagDTO_given_Id()
        {
            var output1 = _repository.Read(5);
            var expected1 = new TagDTO(5, "Tag number 5");

            var output2 = _repository.Read(3);
            var expected2 = new TagDTO(3, "Tag number 3");

            var output3 = _repository.Read(6);
            var expected3 = new TagDTO(6, "Tag number 6");

            var output4 = _repository.Read(7);
            var expected4 = new TagDTO(7, "Tag number 7");

            Assert.Equal(expected1, output1);
            Assert.Equal(expected2, output2);
            Assert.Equal(expected3, output3);
            Assert.Equal(expected4, output4);

        }

        [Fact]
        public void Tag_Update_returns_responseUpdated()
        {
            
            var output = _repository.Update(new TagUpdateDTO());
            Assert.Equal(Response.Updated, output);
        }
    }
}