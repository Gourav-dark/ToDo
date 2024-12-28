using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;

namespace ToDo.UI.Services;
public interface IUserService
{
    Task<string> Login(UserDTO obj);
    Task<bool> Logout();
    Task<string> Register(UserDTO obj);
    Task<User> Get(string userId);
    Task<IEnumerable<User>> GetAll();
}
public class UserService : IUserService
{
    private readonly IDataService<User, string> _data;
    public UserService(IDataService<User, string> data)
    {
        _data = data;
    }
    public async Task<string> Login(UserDTO obj)
    {
        return "";
    }

    public async Task<bool> Logout()
    {
        return false;
    }

    public async Task<string> Register(UserDTO obj)
    {
        User user=new User();
        if (obj == null)
            return "";
        user.UserId = Guid.NewGuid().ToString();
        if (obj.UserType == UserType.Guest)
        {
            user.Email = user.UserId + "@gmail.com";
            user.Password = string.Empty;
        }
        else
        {
            user.Email = obj.Email;
            user.Password = obj.Password;
        }
        user.CreatedAt = DateTime.UtcNow;
        await _data.AddAsync(user);
        return user.UserId;
    }

    public async Task<User> Get(string userId)
    {
        User user = await _data.GetByIdAsync(userId);
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _data.GetAsync();
    }
}
