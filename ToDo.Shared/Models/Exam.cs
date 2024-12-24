namespace ToDo.Shared.Models;
public class Exam
{
    public int ExamId { get; set; }
    public string ExamName { get; set; } = string.Empty;
    public DateTime ApplicationDeadline { get; set; }
    public DateTime ExamDate { get; set; }
    public DateTime ReminderDate { get; set; }
    public bool CountdownWidget { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}