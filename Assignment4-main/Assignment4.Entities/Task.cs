using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Assignment4.Entities
{
    public class Task
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public User? AssignedTo { get; set; }
        
        public string? Description { get; set; }
        
        [Required]
        public State MyState { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }

}
