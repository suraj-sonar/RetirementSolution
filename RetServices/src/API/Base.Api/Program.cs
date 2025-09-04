using Base.logging;
using Base.Application.ServiceContracts;
using Base.Application.Services;
using Base.Application.RepositoryContracts;
using Serilog;
using Base.Persistence;
using Base.Application;
using Microsoft.Extensions.DependencyInjection;
using Base.Application.Logging;
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

    #region loging
    app.Use(async (context, next) =>
    {
        var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                                            .CreateLogger("RequestLogger");
        

        logger.LogInformation("Handling request: {Method} {Path}",
                              context.Request.Method, context.Request.Path);

        await next();

        logger.LogInformation("Finished handling request.");
    });

    #endregion

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
