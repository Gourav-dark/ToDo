namespace ToDo.Shared.DTO;
public class ExamDTO
{
    public string UserId { get; set; } = string.Empty;
    public string ExamName { get; set; } = string.Empty;
    public DateTime ApplicationDeadline { get; set; }
    public DateTime ExamDate { get; set; }
    public DateTime ReminderDate { get; set; }
    public bool CountdownWidget { get; set; } = false;
}
