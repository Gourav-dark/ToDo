//using Microsoft.AspNetCore.Components.Authorization;
//using System.Security.Claims;
//using ToDo.Shared.Models;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.Maui.Storage;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using System.Text.Json;

//namespace ToDo.UI.Authentication;
//public class CustomAuthenticationStateProvider : AuthenticationStateProvider
//{
//    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
//    private const string SecretKey = "fB7$!4f^T8GqLw9hX@3zPvR%Mk*dVpKJ6R2J8NsWyT7#MqX!L3Xq*7f$!9VrPgNz";

//    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//    {
//        var userToken = await SecureStorage.Default.GetAsync("UserToken");

//        if (string.IsNullOrEmpty(userToken))
//        {
//            return new AuthenticationState(_anonymous);
//        }

//        var claimsPrincipal = new ClaimsPrincipal(GetUserClaimsIdentity(userToken));
//        return new AuthenticationState(claimsPrincipal);
//    }

//    public async Task LoginAsync(User user)
//    {
//        // Generate the JWT token
//        var token = GenerateJwtToken(user);

//        // Store the token securely
//        await SecureStorage.Default.SetAsync("UserToken", token);

//        // Notify about the new authentication state
//        var claimsPrincipal = new ClaimsPrincipal(GetUserClaimsIdentity(token));
//        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//    }

//    public async Task LogoutAsync()
//    {
//        // Clear the token
//        SecureStorage.Default.Remove("UserToken");
//        // Notify about the logout state
//        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
//    }

//    private ClaimsIdentity GetUserClaimsIdentity(string token)
//    {
//        var handler = new JwtSecurityTokenHandler();
//        var readToken = handler.ReadJwtToken(token);
//        var claims = readToken.Claims;

//        return new ClaimsIdentity(claims, "CustomAuth");
//    }

//    private string GenerateJwtToken(User user)
//    {
//        var claims = new[]
//        {
//            new Claim(ClaimTypes.NameIdentifier, user.UserId),
//            new Claim(ClaimTypes.Email, user.Email),
//            //new Claim(ClaimTypes.Name, user.Email),
//            new Claim(ClaimTypes.Role, user.UserType.ToString())
//        };

//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
//        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(claims),
//            Expires = DateTime.UtcNow.AddHours(24*7*30),
//            SigningCredentials = credentials
//        };

//        var tokenHandler = new JwtSecurityTokenHandler();
//        var token = tokenHandler.CreateToken(tokenDescriptor);

//        return tokenHandler.WriteToken(token);
//    }

//    public async Task<User?> GetAuthenticatedUserAsync()
//    {
//        var authState = await GetAuthenticationStateAsync();
//        var user = authState.User;

//        if (!user.Identity?.IsAuthenticated ?? true)
//        {
//            return null; // User is not authenticated
//        }

//        return new User
//        {
//            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
//            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
//            UserType = Enum.TryParse(user.FindFirst(ClaimTypes.Role)?.Value, out UserType role) ? role : UserType.Guest,
//            //CreatedAt = DateTime.UtcNow // Adjust if you store this info elsewhere
//        };
//    }
//}
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ToDo.Shared.Models;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace ToDo.UI.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userJson = await SecureStorage.Default.GetAsync("UserData");

            if (string.IsNullOrEmpty(userJson))
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            var user = JsonSerializer.Deserialize<User>(userJson);
            var claimsPrincipal = new ClaimsPrincipal(GetUserClaimsIdentity(user));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task LoginAsync(User user)
    {
        // Serialize the user data
        var userJson = JsonSerializer.Serialize(user);

        // Store the user data securely
        await SecureStorage.Default.SetAsync("UserData", userJson);

        // Notify about the new authentication state
        var claimsPrincipal = new ClaimsPrincipal(GetUserClaimsIdentity(user));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task LogoutAsync()
    {
        // Clear the stored user data
        SecureStorage.Default.Remove("UserData");

        // Notify about the logout state
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private ClaimsIdentity GetUserClaimsIdentity(User? user)
    {
        if (user == null)
        {
            return new ClaimsIdentity();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserType.ToString())
        };

        return new ClaimsIdentity(claims, "CustomAuth");
    }

    public async Task<User?> GetAuthenticatedUserAsync()
    {
        var authState = await GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            return null; // User is not authenticated
        }

        return new User
        {
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
            UserType = Enum.TryParse(user.FindFirst(ClaimTypes.Role)?.Value, out UserType role) ? role : UserType.Guest
        };
    }
}

