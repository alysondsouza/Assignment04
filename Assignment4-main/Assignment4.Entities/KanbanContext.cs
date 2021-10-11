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


            // modelBuilder.Entity<TagTask>()
            //     .HasKey(tt => new { tt.TagId, tt.TaskId });  
            // modelBuilder.Entity<TagTask>()
            //     .HasOne(tt => tt.Tag)
            //     .WithMany(t => t.TagTasks)
            //     .HasForeignKey(tt => tt.TagId);  
            // modelBuilder.Entity<TagTask>()
            //     .HasOne(tt => tt.Task)
            //     .WithMany(ta => ta.TagTasks)
            //     .HasForeignKey(tt => tt.TaskId);