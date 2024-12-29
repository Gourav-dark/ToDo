namespace ToDo.Shared.DTO;
public class TaskItemDTO
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueTime { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public bool IsDailyTask { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    public int CategoryId { get; set; }
}
