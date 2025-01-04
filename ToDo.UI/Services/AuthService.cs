using Microsoft.AspNetCore.Components.Authorization;
using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;
using ToDo.Shared.Wrapper;
using ToDo.UI.Authentication;

namespace ToDo.UI.Services;

public interface IAuthService
{
    Task<ResponseWrapper<User>> Login(UserDTO obj);
    Task Logout();
    Task<ResponseWrapper<User>> Register(UserDTO obj);
    Task<ResponseWrapper<User>> Get(string userId);
    Task<ResponseWrapper<List<User>>> GetAll();
    Task<ResponseWrapper<User>> GetCurrentUser();
}

public class AuthService : IAuthService
{
    private readonly IDataService<User, string> _data;
    private readonly CustomAuthenticationStateProvider _authProvider;

    public AuthService(IDataService<User, string> data, AuthenticationStateProvider authProvider)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _authProvider = authProvider as CustomAuthenticationStateProvider
            ?? throw new ArgumentException("Invalid AuthenticationStateProvider", nameof(authProvider));
    }

    public async Task<ResponseWrapper<User>> Login(UserDTO obj)
    {
        if (obj == null)
            return new ResponseWrapper<User> { Succeeded = false, Message = "Invalid login request." };

        var user = (await _data.GetAsync(x => x.Email == obj.Email)).FirstOrDefault();
        if (user == null)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "User not found."
            };
        }

        if (user.Password != obj.Password)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "Incorrect password."
            };
        }

        await _authProvider.LoginAsync(user);
        return new ResponseWrapper<User>(user)
        {
            Message = "Login successful."
        };
    }

    public async Task Logout()
    {
        await _authProvider.LogoutAsync();
    }

    public async Task<ResponseWrapper<User>> Register(UserDTO obj)
    {
        if (obj == null)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "Invalid registration request."
            };
        }

        // Check if the email already exists in the database
        var existingUser = await _data.GetAsync(u => u.Email == obj.Email);
        if (existingUser == null)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "Email already exists."
            };
        }

        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            Email = obj.UserType == UserType.Guest ? $"{Guid.NewGuid()}@gmail.com" : obj.Email,
            Password = obj.UserType == UserType.Guest ? string.Empty : obj.Password,
            CreatedAt = DateTime.UtcNow,
            UserType=obj.UserType
        };

        try
        {
            var createdUser = await _data.AddAsync(user);
            await _authProvider.LoginAsync(createdUser);

            return new ResponseWrapper<User>(createdUser)
            {
                Message = "Registration successful."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = $"Registration failed: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<User>> Get(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "User ID is required."
            };
        }

        var user = await _data.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "User not found."
            };
        }

        return new ResponseWrapper<User>(user);
    }

    public async Task<ResponseWrapper<List<User>>> GetAll()
    {
        var users = await _data.GetAsync();
        return new ResponseWrapper<List<User>>(users);
    }

    public async Task<ResponseWrapper<User>> GetCurrentUser()
    {
        var user = await _authProvider.GetAuthenticatedUserAsync();
        if (user == null)
        {
            return new ResponseWrapper<User>
            {
                Succeeded = false,
                Message = "No user is currently authenticated."
            };
        }

        return new ResponseWrapper<User>(user);
    }
}
