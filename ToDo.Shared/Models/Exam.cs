using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public class Exam
{
    [Key]
    public int ExamId { get; set; }
    [MaxLength(200)]
    public string ExamName { get; set; } = string.Empty;
    public DateTime ApplicationDeadline { get; set; }
    public DateTime ExamDate { get; set; }
    public DateTime ReminderDate { get; set; }
    public bool CountdownWidget { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}