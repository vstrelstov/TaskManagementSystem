using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Infrastructure.Entities;

namespace TaskManagementSystem.Infrastructure.Contexts
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options) : base(options)
        {
        }

        public DbSet<ManagedTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ManagedTask>().HasKey(x => x.Id);
        }
        
    }
}
