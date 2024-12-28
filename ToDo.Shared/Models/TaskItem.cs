using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Models;

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
public class TaskItem
{
    [Key]
    public int TaskId { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueTime { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public bool IsDailyTask { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
