namespace ToDo.Shared.Models;

public class SecureData
{
    public int Id { get; set; }
    public string? SiteName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; }=string.Empty;
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}
