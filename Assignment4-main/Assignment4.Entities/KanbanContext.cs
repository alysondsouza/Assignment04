using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public class KanbanContext
    {
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.MyState)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)State.Parse(typeof(State), v));
        }
        
//Public DBset indholder tags (liste) (prop)
    }
}
