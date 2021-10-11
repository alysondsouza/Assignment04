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
            return (Response.Created, user.Id);    
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
            return temp.ToArray()[0];
        }
        public Response Update(UserUpdateDTO user)
        {
            throw new NotImplementedException();
        }
        public Response Delete(int userId, bool force = false)
        {
            throw new NotImplementedException();
        }


    }
}
