using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;
public class TaskItemDTO
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueTime { get; set; } = DateTime.UtcNow.AddDays(1);
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public bool IsDailyTask { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    [Required]
    public int CategoryId { get; set; } = 1;
}
