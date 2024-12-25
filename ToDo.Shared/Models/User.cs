using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public enum UserType
{
    Admin,
    User,
    Guest,
}
public class User
{
    [Key]
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}