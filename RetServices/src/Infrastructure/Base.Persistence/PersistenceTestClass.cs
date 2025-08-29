using Base.Application.Logging;
using Base.Application.RepositoryContracts;

namespace Base.Persistence
{
    public class PersistenceTestClass: IPersistenceTestClass
    {
        private readonly IAppLogger<PersistenceTestClass> _logger;
        //constructor
        public PersistenceTestClass(IAppLogger<PersistenceTestClass> logger)
        {
            _logger = logger;
            _logger.LogInformation("PersistenceTestClass instantiated");
        }
        public int PersistenceMethod()
        {
            _logger.LogInformation("PersistenceMethod called");
            return 1;
        }
    }
}