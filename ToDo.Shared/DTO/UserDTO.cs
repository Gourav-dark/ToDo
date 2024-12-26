using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;
public class UserDTO
{
    [Required]
    public string Email { get; set; }=string.Empty;
    [Required]
    public string Password { get; set; }=string.Empty;
    public UserType UserType { get; set; }
}
