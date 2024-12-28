using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public string? UserId { get; set; } =null;
    public string? ColorCode { get; set; }
    public User? User { get; set; }
}