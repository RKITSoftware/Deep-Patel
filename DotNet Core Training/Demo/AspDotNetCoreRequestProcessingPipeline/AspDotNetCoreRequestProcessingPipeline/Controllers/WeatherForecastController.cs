using AspDotNetCoreRequestProcessingPipeline.Filter;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreRequestProcessingPipeline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Log]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [TypeFilter(typeof(MyExceptionFilter))]
        public IEnumerable<WeatherForecast> Get()
        {
            // throw new Exception("bnsjbjkbkdbb");

            _logger.LogInformation("Returing the weather information.");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}