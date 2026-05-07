using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KazanRealEstate.Api.Repositories;
using KazanRealEstate.Api.Repositories.Tables;
using KazanRealEstate.Api.Common;
using BCrypt.Net;
using KazanRealEstate.Api.DTOs;

namespace KazanRealEstate.Api.Services;

public interface IAuthService
{
    Task<Result> RegisterAsync(string username, string password, string email);
    Task<Result<LoginResponseDto>> LoginAsync(string username, string password);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<Result> RegisterAsync(string username, string password, string email)
    {
        if (await _userRepository.ExistsAsync(username))
            return Result.Failure("Пользователь уже существует", 400);

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Email = email
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return Result<LoginResponseDto>.Failure("Неверное имя пользователя или пароль", 401);

        return Result<LoginResponseDto>.Success(new LoginResponseDto(GenerateJwtToken(user)));
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
