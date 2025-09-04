using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Base.Persistence.DBContext;
using Base.Persistence.Repository;
using Base.Application.RepositoryContracts;
using Microsoft.Extensions.Configuration;
namespace Base.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<DapperDbContext>();
        // Register repositories
        services.AddScoped<IPersonRepository, PersonRepository>();
        return services;
    }
}
