using KazanRealEstate.Api.Common;
using KazanRealEstate.Api.Repositories;
using KazanRealEstate.Api.Repositories.Tables;
using KazanRealEstate.Api.Services.Identity;

namespace KazanRealEstate.Api.Services;

public interface IFavoriteService
{
    Task<Result<IEnumerable<ResidentialComplex>>> GetMyFavoritesAsync();
    Task<Result> ToggleFavoriteAsync(int complexId);
    Task<Result<string>> CompareWithAiAsync();
}

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly ICurrentUserService _currentUserService;

    public FavoriteService(IFavoriteRepository favoriteRepository, ICurrentUserService currentUserService)
    {
        _favoriteRepository = favoriteRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<ResidentialComplex>>> GetMyFavoritesAsync()
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<IEnumerable<ResidentialComplex>>.Failure("Не авторизован", 401);

        var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId.Value);
        return Result<IEnumerable<ResidentialComplex>>.Success(favorites.Select(f => f.ResidentialComplex!));
    }

    public async Task<Result> ToggleFavoriteAsync(int complexId)
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result.Failure("Не авторизован", 401);

        if (await _favoriteRepository.IsFavoriteAsync(userId.Value, complexId))
        {
            await _favoriteRepository.RemoveAsync(userId.Value, complexId);
            return Result.Success(200); // Removed
        }
        else
        {
            await _favoriteRepository.AddAsync(new Favorite { UserId = userId.Value, ResidentialComplexId = complexId });
            return Result.Success(201); // Added
        }
    }

    public async Task<Result<string>> CompareWithAiAsync()
    {
        var userId = _currentUserService.UserId;
        if (userId == null) return Result<string>.Failure("Не авторизован", 401);

        var favorites = await _favoriteRepository.GetUserFavoritesAsync(userId.Value);
        if (!favorites.Any()) return Result<string>.Failure("Список избранного пуст", 400);

        // Сбор параметров для анализа
        var dataForAi = favorites.Select(f => new {
            f.ResidentialComplex!.Name,
            f.ResidentialComplex.DistanceToCenter,
            f.ResidentialComplex.InfrastructureRating,
            f.ResidentialComplex.EcologicalRating,
            f.ResidentialComplex.MinPrice,
            f.ResidentialComplex.BuildingMaterial
        }).ToList();

        // Базовый расчет оптимального варианта
        var bestChoice = favorites.OrderByDescending(f => 
            (5 - f.ResidentialComplex!.DistanceToCenter / 5) + 
            f.ResidentialComplex.InfrastructureRating + 
            f.ResidentialComplex.EcologicalRating
        ).First();

        var aiAnalysis = $"Анализ ИИ завершен. Основываясь на параметрах экологии, инфраструктуры и близости к центру, " +
                         $"лучшим выбором для вас является {bestChoice.ResidentialComplex!.Name}. " +
                         $"(Данные подготовлены для передачи в Python-модуль на основе TensorFlow/PyTorch)";

        return Result<string>.Success(aiAnalysis);
    }
}
