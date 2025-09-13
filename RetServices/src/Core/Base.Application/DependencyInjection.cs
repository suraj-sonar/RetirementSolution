using Base.Application.ServiceContracts;
using Base.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Base.Application.Validation;
using FluentValidation.AspNetCore;

namespace Base.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
       
        services.AddValidatorsFromAssemblyContaining<PersonValidator>();
        //services.AddFluentValidationAutoValidation();
        
        
        return services;
    }
}
