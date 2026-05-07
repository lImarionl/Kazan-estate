using Microsoft.EntityFrameworkCore;
using KazanRealEstate.Api.Data;
using KazanRealEstate.Api.Repositories.Tables;

namespace KazanRealEstate.Api.Repositories;

public interface IFavoriteRepository
{
    Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId);
    Task<bool> AddAsync(Favorite favorite);
    Task<bool> RemoveAsync(int userId, int complexId);
    Task<bool> IsFavoriteAsync(int userId, int complexId);
}

public class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _context;

    public FavoriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId)
    {
        return await _context.Favorites
            .Include(f => f.ResidentialComplex)
                .ThenInclude(c => c!.Developer)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> AddAsync(Favorite favorite)
    {
        if (await IsFavoriteAsync(favorite.UserId, favorite.ResidentialComplexId))
            return false;

        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveAsync(int userId, int complexId)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.ResidentialComplexId == complexId);

        if (favorite == null) return false;

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsFavoriteAsync(int userId, int complexId)
    {
        return await _context.Favorites.AnyAsync(f => f.UserId == userId && f.ResidentialComplexId == complexId);
    }
}
