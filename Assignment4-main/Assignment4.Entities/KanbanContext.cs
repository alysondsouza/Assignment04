using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public class KanbanContext : DbContext //DERIVES
    { 
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.MyState)
                .HasConversion(
                    v => v.ToString(),
                    v => (State)State.Parse(typeof(State), v));

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
        }     
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) {}

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<TagTask> TagTask { get; set; }


    }
}
