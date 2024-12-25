using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public enum TodoStatus
{
    Pending,
    InProgress,
    Completed,
    Overdue
}
public enum TodoPriority
{
    Low,
    Medium,
    High,
    Critical
}
public class TodoItem
{
    [Key]
    public int TodoId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueTime { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}