using KazanRealEstate.Api.DTOs;
using KazanRealEstate.Api.Repositories;
using KazanRealEstate.Api.Repositories.Tables;
using KazanRealEstate.Api.Common;
using KazanRealEstate.Api.Services.Identity;

namespace KazanRealEstate.Api.Services;

public interface IUserService
{
    Task<Result<UserProfileDto>> GetProfileAsync();
    Task<Result> UpdateProfileAsync(UpdateProfileDto model);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UserService(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UserProfileDto>> GetProfileAsync()
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<UserProfileDto>.Failure("Не авторизован", 401);

        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user == null) 
            return Result<UserProfileDto>.Failure("Пользователь не найден", 404);

        return Result<UserProfileDto>.Success(new UserProfileDto
        {
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber
        });
    }

    public async Task<Result> UpdateProfileAsync(UpdateProfileDto model)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Не авторизован", 401);

        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user == null) 
            return Result.Failure("Пользователь не найден", 404);

        if (!string.IsNullOrEmpty(model.Email)) user.Email = model.Email;
        user.FullName = model.FullName;
        user.PhoneNumber = model.PhoneNumber;

        await _userRepository.SaveChangesAsync();
        return Result.Success();
    }
}
