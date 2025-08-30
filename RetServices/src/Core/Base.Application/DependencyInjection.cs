using Base.Application.ServiceContracts;
using Base.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
        return services;
    }
}
