using KazanRealEstate.Api.Repositories.Tables;
using KazanRealEstate.Api.Repositories;
using KazanRealEstate.Api.Common;

namespace KazanRealEstate.Api.Services;

public interface IPropertyService
{
    Task<Result<IEnumerable<ResidentialComplex>>> GetComplexesAsync();
    Task<Result<IEnumerable<Developer>>> GetDevelopersAsync();
}

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _repository;

    public PropertyService(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ResidentialComplex>>> GetComplexesAsync()
    {
        var data = await _repository.GetAllComplexesAsync();
        return Result<IEnumerable<ResidentialComplex>>.Success(data);
    }

    public async Task<Result<IEnumerable<Developer>>> GetDevelopersAsync()
    {
        var data = await _repository.GetAllDevelopersAsync();
        return Result<IEnumerable<Developer>>.Success(data);
    }
}
