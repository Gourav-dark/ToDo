using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}