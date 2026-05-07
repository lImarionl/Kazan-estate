using Microsoft.EntityFrameworkCore;
using KazanRealEstate.Api.Data;
using KazanRealEstate.Api.Repositories.Tables;

namespace KazanRealEstate.Api.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<ResidentialComplex>> GetAllComplexesAsync();
    Task<IEnumerable<Developer>> GetAllDevelopersAsync();
}

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ResidentialComplex>> GetAllComplexesAsync()
    {
        return await _context.ResidentialComplexes.Include(c => c.Developer).ToListAsync();
    }

    public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
    {
        return await _context.Developers.ToListAsync();
    }
}
