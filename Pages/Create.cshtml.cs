using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerProject.Data; // Import database context
using TaskManagerProject.Models; // Import the TaskItem model

namespace TaskManagerProject.Pages
{
    // Handling task creation
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context) => _context = context;

        // Representing the new task
        [BindProperty]
        public TaskItem Task { get; set; } = new();

        // Create valid task
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            // Add the new task to the database and save changes
            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}