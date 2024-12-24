namespace ToDo.Shared.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}