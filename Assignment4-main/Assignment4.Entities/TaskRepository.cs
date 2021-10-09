using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        private readonly KanbanContext _context;
        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            return (Response.Created, (int)task.AssignedToId);
        }
        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            var temp = _context.tasks
            .Include(x => x.Tags)
            .Select(i =>
            new TaskDTO(
                i.Id,
                i.Title,
                i.AssignedTo.Name,
                i.Tags.Select(y => y.Name).ToArray(),
                i.MyState));

            return temp.ToList();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            var temp = from task in _context.tasks
                       where task.MyState == State.Removed
                       select new TaskDTO(task.Id, task.Title, task.AssignedTo.Name, task.Tags.Select(y => y.Name).ToArray(), task.MyState);
            return temp.ToList();
        }
        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            var temp = from task in _context.tasks
                       select new TaskDTO(task.Id, task.Title, task.AssignedTo.Name, task.Tags.Select(y => y.Name).ToArray(), task.MyState);

            var temp2 = new List<TaskDTO>();

            foreach (var task in temp)
            {
                foreach (var t in task.Tags)
                {
                    if (t.Equals(tag))
                    {
                        temp2.Add(task);
                        break;
                    }
                }
            }
            return temp2;
        }
        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            var temp = from task in _context.tasks
                       where task.AssignedTo.Id == userId
                       select new TaskDTO(task.Id, task.Title, task.AssignedTo.Name, task.Tags.Select(y => y.Name).ToArray(), task.MyState);

            return temp.ToList();
        }
        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            var temp = from task in _context.tasks
                       where task.MyState == state
                       select new TaskDTO(task.Id, task.Title, task.AssignedTo.Name, task.Tags.Select(y => y.Name).ToArray(), task.MyState);

            return temp.ToList();
        }

        public TaskDetailsDTO Read(int taskId)
        {
            var temp = from task in _context.tasks
                       where task.Id == taskId
                       select new TaskDetailsDTO(task.Id, task.Title, task.Description, new DateTime(), task.AssignedTo.Name, task.Tags.Select(y => y.Name).ToArray(), task.MyState, new DateTime());
            return temp.ToList()[0];
        }
        public Response Update(TaskUpdateDTO task)
        {
            /* _context.tasks.Select(y =>)
            _context.tasks.Remove(temp.ToList()[0]); */
            throw new NotImplementedException();

        }
        public Response Delete(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
