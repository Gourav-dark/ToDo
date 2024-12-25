using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Models;

namespace ToDo.Shared.DTO;
public class UserDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public UserType UserType { get; set; }
}
