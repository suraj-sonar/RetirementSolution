using Base.Application.RepositoryContracts;
using Base.Application.ServiceContracts;
using Base.Domain;
using Microsoft.Extensions.Logging;
namespace Base.Application.Services;

public class PersonAddressService : IPersonAddressService
{
    private readonly IPersonAddressRepository _repository;
    private readonly ILogger<PersonAddressService> _logger;

    public PersonAddressService(IPersonAddressRepository repository, ILogger<PersonAddressService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<PersonAddress>> GetAllAsync()
    {
        _logger.LogInformation("Getting all person addresses.");
        return await _repository.GetAllAsync();
    }

    public async Task<PersonAddress> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Getting person address with ID: {id}");
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> AddAsync(PersonAddress address)
    {
        _logger.LogInformation("Adding a new person address.");
        return await _repository.AddAsync(address);
    }

    public async Task<bool> UpdateAsync(PersonAddress address)
    {
        _logger.LogInformation($"Updating person address with ID: {address.id}");
        return await _repository.UpdateAsync(address);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation($"Deleting person address with ID: {id}");
        return await _repository.DeleteAsync(id);
    }
}
