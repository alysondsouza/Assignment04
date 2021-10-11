using System;
using Xunit;

using Assignment4.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests
    {
        private readonly KanbanContext _context;
        private readonly TaskRepository _repository;
        public TaskRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();


            var caspar = new User
            {
                Id = 1,
                Name = "Caspar",
                Email = "cafm@itu.dk",
                Tasks = new List<Task>()
            };

            var ali = new User
            {
                Id = 2,
                Name = "Alyson",
                Email = "ades@itu.dk",
                Tasks = new List<Task>()
            };

            var mads = new User
            {
                Id = 3,
                Name = "Mads",
                Email = "mpia@itu.dk",
                Tasks = new List<Task>()
            };

            var tag1 = new Tag
            {
                Id = 1,
                Name = "tag number 1"
            };

            var tag2 = new Tag
            {
                Id = 2,
                Name = "tag number 2"
            };

            var tag3 = new Tag
            {
                Id = 3,
                Name = "tag number 3"
            };

            var tag4 = new Tag
            {
                Id = 4,
                Name = "tag number 4"
            };

            var tag5 = new Tag
            {
                Id = 5,
                Name = "tag number 5"
            };
            context.tasks.AddRange(
            new Task
            {
                Id = 1,
                Title = "First task",
                AssignedTo = caspar,
                Description = "This task needs to be done",
                MyState = State.New,
                Tags = new[] { tag1, tag2 }
            },
            new Task
            {
                Id = 2,
                Title = "Second task",
                AssignedTo = ali,
                Description = "This task needs to be done quickly",
                MyState = State.Removed,
                Tags = new[] { tag3, tag4 }
            },
            new Task
            {
                Id = 3,
                Title = "Third task",
                AssignedTo = mads,
                Description = "Please finish this task",
                MyState = State.Active,
                Tags = new[] { tag1, tag5 }
            },
            new Task
            {
                Id = 4,
                Title = "Fourth task",
                AssignedTo = caspar,
                Description = "More tasks",
                MyState = State.Resolved,
                Tags = new[] { tag1, tag2, tag3, tag4 }
            });

            context.SaveChanges();
            _context = context;
            _repository = new TaskRepository(_context);

        }

        [Fact]
        public void Create_returns_correct_tuple()
        {
            var repository = new TaskRepository(_context);

            var myDTO = new TaskCreateDTO()
            {
                Title = "My Title",
                AssignedToId = 8,
                Description = "this is a desxcription",
                Tags = new[] {
                "myTag",
                "someTag",
                "thisTag"
                }
            };
            var output = repository.Create(myDTO);
            Assert.Equal((Response.Created, 8), output);
        }

        [Fact]
        public void ReadAll_returns_all_tasks_in_repo()
        {
            //Cannot use Assert.equal on objects with ICollection fields. Do not know why
            //Converted all objects to JSON strings
            var tasks = _repository.ReadAll();
            var temp1 = JsonConvert.SerializeObject(new TaskDTO(1, "First task", "Caspar", new[] { "tag number 1", "tag number 2" }, State.New));
            var temp2 = JsonConvert.SerializeObject(new TaskDTO(2, "Second task", "Alyson", new[] { "tag number 3", "tag number 4" }, State.Removed));
            var temp3 = JsonConvert.SerializeObject(new TaskDTO(3, "Third task", "Mads", new[] { "tag number 1", "tag number 5" }, State.Active));
            var temp4 = JsonConvert.SerializeObject(new TaskDTO(4, "Fourth task", "Caspar", new[] { "tag number 1", "tag number 2", "tag number 3", "tag number 4" }, State.Resolved));

            Assert.Collection(tasks,
                t => Assert.Equal(temp1, JsonConvert.SerializeObject(t)),
                t => Assert.Equal(temp2, JsonConvert.SerializeObject(t)),
                t => Assert.Equal(temp3, JsonConvert.SerializeObject(t)),
                t => Assert.Equal(temp4, JsonConvert.SerializeObject(t))

            );
        }

        [Fact]
        public void ReadAllRemoved_Returns_One_Object()
        {
            var tasks = _repository.ReadAllRemoved();
            var temp1 = JsonConvert.SerializeObject(new TaskDTO(2, "Second task", "Alyson", new[] { "tag number 3", "tag number 4" }, State.Removed));

            Assert.Collection(tasks,
            t => Assert.Equal(temp1, JsonConvert.SerializeObject(t))
            );
        }

        [Fact]
        public void ReadAllByTag_returns_three_tasks_given_tag1()
        {
            var tasks = _repository.ReadAllByTag("tag number 1");
            var temp1 = JsonConvert.SerializeObject(new TaskDTO(1, "First task", "Caspar", new[] { "tag number 1", "tag number 2" }, State.New));
            var temp2 = JsonConvert.SerializeObject(new TaskDTO(3, "Third task", "Mads", new[] { "tag number 1", "tag number 5" }, State.Active));
            var temp3 = JsonConvert.SerializeObject(new TaskDTO(4, "Fourth task", "Caspar", new[] { "tag number 1", "tag number 2", "tag number 3", "tag number 4" }, State.Resolved));

            Assert.Collection(tasks,
            t => Assert.Equal(temp1, JsonConvert.SerializeObject(t)),
            t => Assert.Equal(temp2, JsonConvert.SerializeObject(t)),
            t => Assert.Equal(temp3, JsonConvert.SerializeObject(t))
            );
        }

        [Fact]
        public void ReadByUser_returns_two_tasks_given_caspar()
        {
            var tasks = _repository.ReadAllByUser(1);
            var temp1 = JsonConvert.SerializeObject(new TaskDTO(1, "First task", "Caspar", new[] { "tag number 1", "tag number 2" }, State.New));
            var temp2 = JsonConvert.SerializeObject(new TaskDTO(4, "Fourth task", "Caspar", new[] { "tag number 1", "tag number 2", "tag number 3", "tag number 4" }, State.Resolved));

            Assert.Collection(tasks,
            t => Assert.Equal(temp1, JsonConvert.SerializeObject(t)),
            t => Assert.Equal(temp2, JsonConvert.SerializeObject(t))
            );

        }

        [Fact]
        public void ReadAllByState_Returns_one_task_given_a_State()
        {
            var tasks1 = _repository.ReadAllByState(State.New);
            var tasks2 = _repository.ReadAllByState(State.Removed);
            var tasks3 = _repository.ReadAllByState(State.Active);

            var temp1 = JsonConvert.SerializeObject(new TaskDTO(1, "First task", "Caspar", new[] { "tag number 1", "tag number 2" }, State.New));
            var temp2 = JsonConvert.SerializeObject(new TaskDTO(2, "Second task", "Alyson", new[] { "tag number 3", "tag number 4" }, State.Removed));
            var temp3 = JsonConvert.SerializeObject(new TaskDTO(3, "Third task", "Mads", new[] { "tag number 1", "tag number 5" }, State.Active));

            Assert.Collection(tasks1,
            t => Assert.Equal(temp1, JsonConvert.SerializeObject(t))
            );

            Assert.Collection(tasks2,
            t => Assert.Equal(temp2, JsonConvert.SerializeObject(t))
            );

            Assert.Collection(tasks3,
            t => Assert.Equal(temp3, JsonConvert.SerializeObject(t))
            );

        }

        [Fact]
        public void Task_Update_changes_State_of_task()
        {
            var repository = new TaskRepository(_context);
            Assert.Equal(State.Active, repository.Read(3).State);
            var updateDTO = new TaskUpdateDTO { Id = 3, State = State.Closed};
            var resp = repository.Update(updateDTO);
            Assert.Equal(Response.Updated, resp);
            Assert.Equal(State.Closed, repository.Read(3).State);

        }

        [Fact]
        public void Task_Delete_returns_Response_given_Id()
        {
            var repository = new TaskRepository(_context);

            var arrayTags = repository.ReadAll();
            Assert.Equal(4, arrayTags.Count);
            var answer = repository.Delete(3);

            var finalArray = repository.ReadAll();
            Assert.Equal(3, finalArray.Count);

            Assert.Equal(Response.Deleted, answer);
        }

    }
}
