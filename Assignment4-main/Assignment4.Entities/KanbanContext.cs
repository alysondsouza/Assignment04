using Microsoft.EntityFrameworkCore;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class KanbanContext : DbContext
    { //Skal extend

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.MyState)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)State.Parse(typeof(State), v));
            
            //Don't really know if this is necessary, ask TA
            modelBuilder
                .Entity<Tag>().Property(e => e.Id);
                
        }

        public DbSet<Tag> tags { get; set; }
        public DbSet<Task> tasks { get; set; }
        public DbSet<User> users { get; set; }

        
    }
}
