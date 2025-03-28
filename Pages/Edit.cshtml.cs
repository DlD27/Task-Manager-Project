using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagerProject.Data; // Import database context
using TaskManagerProject.Models; // Import the TaskItem model
using System.Threading.Tasks;

namespace TaskManagerProject.Pages
{
    // Handling task editing
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem Task { get; set; }

        // Load the selected task to edit
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Task = await _context.Tasks.FindAsync(id);
            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }

        // Modify valid task
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}