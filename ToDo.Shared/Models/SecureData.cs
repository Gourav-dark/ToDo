using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;
public class SecureData
{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public string? SiteName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? Email { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? Password { get; set; }=string.Empty;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}
