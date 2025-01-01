using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;

public class SecureDataDTO
{
    [Required]
    public string SiteName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
