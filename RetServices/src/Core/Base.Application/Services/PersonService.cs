using Base.Application.Logging;
using Base.Application.RepositoryContracts;
using Base.Application.ServiceContracts;
using Base.Domain;

namespace Base.Application.Services;
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IAppLogger<PersonService> _logger;
    public PersonService(IPersonRepository personRepository, IAppLogger<PersonService> logger)  
    {
        _personRepository = personRepository;
        _logger = logger;
    }
    public Task<int> AddPersonAsync(Person person)
    {
      return _personRepository.AddPersonAsync(person);
    }

    public Task DeletePersonAsync(int id)
    {
        return _personRepository.DeletePersonAsync(id);
    }

    public Task<List<Person>> GetAllPersonsAsync()
    {
        _logger.LogInformation("Fetching all persons from the repository.");
        return _personRepository.GetAllPersonsAsync();
    }

    public Task<Person?> GetPersonByIdAsync(int id)
    {
        return _personRepository.GetPersonByIdAsync(id);
    }

    public Task UpdatePersonAsync(Person person)
    {
        return _personRepository.UpdatePersonAsync(person);
    }

    //private readonly IAppLogger<ApplicationTestClass> _logger;


    //public ApplicationTestClass(IAppLogger<ApplicationTestClass> logger,IPersistenceTestClass persistenceTestClass)
    //{
    //    _logger = logger;
    //    _persistenceTestClass = persistenceTestClass;
    //    _logger.LogInformation("ApplicationTestClass instantiated");
    //}
    //public Task<int> ApplicationMethod()
    //{
    //    _logger.LogInformation("ApplicationMethod called");
    //    _persistenceTestClass.PersistenceMethod();
    //    return Task.FromResult(1);
    //}
    
}

