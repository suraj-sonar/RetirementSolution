
namespace Base.Application.RepositoryContracts;

public interface IPersonRepository
{
    
    public Task<int> AddPersonAsync(Domain.Person person);
    public Task<Domain.Person?> GetPersonByIdAsync(int id);
    public Task<List<Domain.Person>> GetAllPersonsAsync();
    public Task UpdatePersonAsync(Domain.Person person);
    public Task DeletePersonAsync(int id);

}
