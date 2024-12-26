using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;
public class TodoItemDTO
{
    public int CategoryId { get; set; }
    public string? UserId { get; set; } = null;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }=string.Empty;
    public TodoPriority Priority { get; set; }
    public DateTime DueTime { get; set; }
    public TodoStatus Status { get; set; }
}
