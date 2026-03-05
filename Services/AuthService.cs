namespace TodoApi.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Data;
using TodoApi.Models;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _config = configuration;
    }

    public (string? token, string? error) Login(string email, string password)
    {
        User? existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (existingUser == null)
            return (null, "Username or password is incorrect.");

        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(password, existingUser.PasswordHash);

        if (!isCorrectPassword)
            return (null, "Username or password is incorrect.");

        return (GenerateToken(existingUser), null);
    }

    public (string? token, string? error) Register(string email, string password, string name)
    {
        bool existingUser = _context.Users.Any(u => u.Email == email);
        if (existingUser)
            return (null, "Email already in use");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        User newUser = new User
        {
            Email = email,
            PasswordHash = hashedPassword,
            Name = name,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Add(newUser);
        _context.SaveChanges();

        return (GenerateToken(newUser), null);
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
