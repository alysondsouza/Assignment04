using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public class KanbanContext : DbContext
    { //Skal extend
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.MyState)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)State.Parse(typeof(State), v));
        }
        
        public DbSet<Tag> tags { get; set; }
        public DbSet<Task> tasks { get; set; }
        public DbSet<User> users { get; set; }
    }
}
