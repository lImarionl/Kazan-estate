using System.ComponentModel.DataAnnotations;

namespace KazanRealEstate.Api.DTOs;

public record UserProfileDto
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}

public record UpdateProfileDto
{
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    public string Email { get; init; } = string.Empty;
    
    public string FullName { get; init; } = string.Empty;
    
    [RegularExpression(@"^(\+7|7|8)?[\s\-]?\(?[49][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$", ErrorMessage = "Некорректный формат номера телефона. Используйте +7 (999) 999-99-99 или 89999999999")]
    public string PhoneNumber { get; init; } = string.Empty;
}
