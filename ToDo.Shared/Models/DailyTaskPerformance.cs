namespace ToDo.Shared.Models;
public class DailyTaskPerformance
{
    public int PerformanceId { get; set; }
    public DateTime TaskDate { get; set; }
    public int TotalTasks { get; set; } = 0;
    public int CompletedTasks { get; set; } = 0;
    public int MissedTasks { get; set; } = 0;
    public float CompletionPercentage { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}