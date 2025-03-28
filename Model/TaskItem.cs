using System.ComponentModel.DataAnnotations;

namespace TaskManagerProject.Models
{
    public class TaskItem
    {
        public int Id { get; set; }  // Primary Key

        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateDue  { get; set; }
        
        public bool IsCompleted { get; set; } = false;
    }
}