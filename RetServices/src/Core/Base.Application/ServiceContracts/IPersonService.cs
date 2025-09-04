

namespace Base.Application.ServiceContracts;

public interface IPersonService
{
    Task<int> AddPersonAsync(Domain.Person person);
    Task<Domain.Person?> GetPersonByIdAsync(int id);
    Task<List<Domain.Person>> GetAllPersonsAsync();
    Task UpdatePersonAsync(Domain.Person person);
    Task DeletePersonAsync(int id);
}
