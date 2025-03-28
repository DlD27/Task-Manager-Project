using Microsoft.EntityFrameworkCore;
using TaskManagerProject.Models; // Import the TaskItem model

namespace TaskManagerProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}