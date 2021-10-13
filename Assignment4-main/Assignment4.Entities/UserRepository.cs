
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class UserRepository : IUserRepository
    {
        private KanbanContext _context;

        public UserRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int UserId) Create(UserCreateDTO user)
        {
            //BR 4.3
            var entity = _context.users.Find(user.Id);
            if (entity == null)
            {
                _context.users.Add(new User { Id = user.Id, Name = user.Name});
                return (Response.Created, user.Id);
            } else 
            return (Response.Conflict, user.Id);    

        }
        
        public IReadOnlyCollection<UserDTO> ReadAll()
        {
            var temp = _context.users.Select(i => new UserDTO(i.Id, i.Name, i.Email));

            return temp.ToArray();
        }

        public UserDTO Read(int userId)
        {
            var temp = from user in _context.users
                        where user.Id == userId
                        select new UserDTO(user.Id, user.Name, user.Email);
            
            //BR_1.5
            if (temp == null) return null;

            //BR_1.5
            return temp.SingleOrDefault();  

 
        }

        public Response Update(UserUpdateDTO user)
        {
            var entity = _context.users.Find(user.Id);

            //BR_1.1
            if (entity == null) return Response.NotFound;

            entity.Name = user.Name;
            _context.SaveChanges();

            return Response.Updated;
        }

        public Response Delete(int userId, bool force = false)
        {
            var entity = _context.users.Find(userId);

            //BR_1.1
            if (entity == null) return Response.NotFound;
            //BR 4.1 & 4.2
            if (force == false && (entity.Tasks != null)) 
            {
                if (entity.Tasks.Count() > 0) return Response.Conflict;
            }
            _context.users.Remove(entity);
            _context.SaveChanges();
            return Response.Deleted;
        }

    }
}
