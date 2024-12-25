using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;

public class SecureDataDTO
{
    public string SiteName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
