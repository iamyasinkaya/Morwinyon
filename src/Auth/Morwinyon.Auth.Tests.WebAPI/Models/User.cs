using System.ComponentModel.DataAnnotations;

namespace Morwinyon.Auth.Tests.WebAPI.Models;

public class User
{
    [Key]
    public string UserName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Role { get; set; } = "Everyone";
    public bool IsActive { get; set; } = false;
    public string Token { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; }

    public User(string userName, string name, string password, string role, string email)
    {
        UserName = userName;
        Name = name;
        Password = password;
        Role = role;
        Email = email;
    }
}

public class LoginUser
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegisterUser
{
    public string Name { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "Everyone";
    public string Email { get; set; }
}