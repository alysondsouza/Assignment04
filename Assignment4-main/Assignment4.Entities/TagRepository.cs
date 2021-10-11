
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
            //This is probably 99% wrong. Don't know how it works. Ask TA
            _context.Update(tag);
            return Response.Updated;
        }

        public Response Delete(int tagId, bool force = false)
        {
            throw new NotImplementedException();
        }

    }
}
