using System.ComponentModel.DataAnnotations;

namespace KazanRealEstate.Api.DTOs;

public record UserRegisterDto(
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    string Username, 
    
    [Required(ErrorMessage = "Пароль обязателен")]
    string Password, 
    
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    string Email
);
public record UserLoginDto(string Username, string Password);
public record LoginResponseDto(string Token);
