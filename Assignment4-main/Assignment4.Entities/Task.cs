using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public ICollection<Tag> Tags { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.MyState)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)State.Parse(typeof(State), v));
        }

    }

}
