using Morwinyon.Auth.Tests.WebAPI.Models;

namespace Morwinyon.Auth.Tests.WebAPI.AuthService.Abstract;

public interface IAuthService
{
    public Task<User> Login(string email, string password);
    public Task<User> Register(User user);
}