using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        private KanbanContext _context;

        public TagRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int TagId) Create(TagCreateDTO tag)
        {
            var entity = _context.tags.Find(tag.Id);
            if (entity != null) return (Response.Conflict, tag.Id);
    
            _context.tags.Add(new Tag { Id = tag.Id, Name = tag.Name });
            _context.SaveChanges();
            return (Response.Created, tag.Id);
    
        }

        public IReadOnlyCollection<TagDTO> ReadAll()
        {
            var temp = _context.tags.Select(i => new TagDTO(i.Id, i.Name));

            return temp.ToArray();
        }
        public TagDTO Read(int tagId)
        {
            var temp = from tag in _context.tags
                       where tag.Id == tagId
                       select new TagDTO(tag.Id, tag.Name);

            return temp.ToArray()[0];
        }
        public Response Update(TagUpdateDTO tag)
        {
            var entity = _context.tags.Find(tag.Id);

            if (entity == null) return Response.NotFound;

            entity.Name = tag.Name;
            _context.SaveChanges();

            return Response.Updated;
        }

        public Response Delete(int tagId, bool force)
        {

            var entity = _context.tags.Find(tagId);
            if (entity == null) return Response.NotFound;
            if (force == false && (entity.Tasks != null)) {
                if (entity.Tasks.Count() > 0) return Response.Conflict;
            }

            _context.tags.Remove(entity);
            _context.SaveChanges();
            return Response.Deleted;
        }

    }
}
