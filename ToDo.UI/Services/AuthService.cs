using Microsoft.AspNetCore.Components.Authorization;
using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;
using ToDo.UI.Authentication;

namespace ToDo.UI.Services;
public interface IAuthService
{
    Task<bool> Login(UserDTO obj);
    Task Logout();
    Task<string> Register(UserDTO obj);
    Task<User> Get(string userId);
    Task<IEnumerable<User>> GetAll();
}
public class AuthService : IAuthService
{
    private readonly IDataService<User, string> _data;
    private readonly CustomAuthenticationStateProvider _authProvider;
    public AuthService(IDataService<User, string> data, AuthenticationStateProvider authProvider)
    {
        _data = data;
        _authProvider = (CustomAuthenticationStateProvider)authProvider;
    }
    public async Task<bool> Login(UserDTO obj)
    {
        User user = (await _data.GetAsync(x => x.Email == obj.Email && x.Password == obj.Password)).FirstOrDefault(new User());
        if (user == null)
        {
            return false;
        }
        await _authProvider.LoginAsync(user);
        return true;
    }

    public async Task Logout()
    {
        await _authProvider.LogoutAsync();
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
        User currentUser=await _data.AddAsync(user);
        await _authProvider.LoginAsync(user);
        return user.UserId;
    }

    public async Task<User> Get(string userId)
    {
        User? user = await _data.GetByIdAsync(userId);
        if (user == null)
            return new User();
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _data.GetAsync();
    }
}
