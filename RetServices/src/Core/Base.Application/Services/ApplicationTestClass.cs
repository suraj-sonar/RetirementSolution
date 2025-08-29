using Base.Application.Logging;
using Base.Application.RepositoryContracts;
using Base.Application.ServiceContracts;
namespace Base.Application.Services;
public class ApplicationTestClass : IApplicationTestClass
{
    
    private readonly IAppLogger<ApplicationTestClass> _logger;
    private readonly IPersistenceTestClass _persistenceTestClass;

    public ApplicationTestClass(IAppLogger<ApplicationTestClass> logger,IPersistenceTestClass persistenceTestClass)
    {
        _logger = logger;
        _persistenceTestClass = persistenceTestClass;
        _logger.LogInformation("ApplicationTestClass instantiated");
    }
    public Task<int> ApplicationMethod()
    {
        _logger.LogInformation("ApplicationMethod called");
        _persistenceTestClass.PersistenceMethod();
        return Task.FromResult(1);
    }
}

