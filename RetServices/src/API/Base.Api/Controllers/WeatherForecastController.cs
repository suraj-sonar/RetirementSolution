
using Base.Application;
using Base.Application.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Base.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IApplicationTestClass _applicationTestClass;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApplicationTestClass applicationTestClass)
        {
            _logger = logger;
            _applicationTestClass = applicationTestClass;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Fetching weather forecast data");
            //new ApplicationTestClass().ApplicationMethod();
            _applicationTestClass.ApplicationMethod();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
