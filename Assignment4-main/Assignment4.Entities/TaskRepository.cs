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
            //BR_2.4 DateTime

            //BR_2.4
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

            //this has a terrible runtime. Ask TA
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

            //BR_1.5
            if (temp == null) return null;

            //BR_1.5
            return temp.SingleOrDefault();
        }

        public Response Update(TaskUpdateDTO task)
        {
            var entity = _context.tasks.Find(task.Id);

            //BR_1.1
            if (entity == null) return Response.NotFound;

            entity.MyState = task.State;
            return Response.Updated;
        }

        public Response Delete(int taskId)
        {
            var entity = _context.tasks.Find(taskId);

            //BR_1.1
            if (entity == null) return Response.NotFound;

            //BR_2.1, 2.2, 2.3
            switch (entity.MyState){
                case State.New:
                    _context.tasks.Remove(entity);
                    _context.SaveChanges();
                        return Response.Deleted;
                    break;
                case State.Active:
                    entity.MyState = State.Removed;
                    _context.SaveChanges();
                        return Response.Updated;
                    break;
                case State.Resolved:
                case State.Closed:
                case State.Removed:
                    return Response.Conflict;   
                    break;
                default:
                    return Response.BadRequest;
                    break;
            }
        }
    }
}
