using Microsoft.EntityFrameworkCore;
using TaskSystem.Data.Map;
using TaskSystem.Model;

namespace TaskSystem.Data
{
    public class TaskSysDBContext : DbContext
    {
        public TaskSysDBContext(DbContextOptions<TaskSysDBContext> options) : base(options)
        { 

        }

        public DbSet<UserModel> Users { get; set;}
        public DbSet<TaskModel> Tasks { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TaskMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
