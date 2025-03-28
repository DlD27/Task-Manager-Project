using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagerProject.Data; // Import database context
using TaskManagerProject.Models; // Import the TaskItem model
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TaskManagerProject.Pages
{   
    // Handling task management in index page
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) => _context = context;

        // Lists to store pending and completed tasks
        public List<TaskItem> PendingTasks { get; set; } = new();
        public List<TaskItem> CompletedTasks { get; set; } = new();

        // Retrieve tasks from the database and categorize them by completion status
        public async Task OnGetAsync()
        {   
            var tasks = await _context.Tasks
                .OrderBy(t => t.DateDue) // Order tasks by due date
                .ToListAsync();

            // Separate tasks into 'pending' and 'completed' lists
            PendingTasks = tasks.Where(t => !t.IsCompleted).ToList();
            CompletedTasks = tasks.Where(t => t.IsCompleted).ToList();
        }

        // Mark the selected task as completed
        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // Delete the selected task from the database
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}

