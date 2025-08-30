using Base.logging;
using Base.Application.ServiceContracts;
using Base.Application.Services;
using Base.Application.RepositoryContracts;
using Serilog;
using Base.Persistence;
using Base.Application;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppLogging(builder.Configuration);
try
{


    // builder.Services.AddScoped<IApplicationTestClass, ApplicationTestClass>();
    //builder.Services.AddScoped<IPersistenceTestClass, PersistenceTestClass>();
    
    builder.Services.AddControllers();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddCore();
    builder.Services.AddAppLogging(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{

   Log.Logger.Fatal("Application start-up failed");
}
