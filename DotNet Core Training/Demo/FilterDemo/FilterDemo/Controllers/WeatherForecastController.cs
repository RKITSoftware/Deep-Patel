using FilterDemo.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    /// <summary>
    /// Default WeatherForecastController provided by Visual Studio
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [MySampleActionFilter(name: "Sync9 Filter")]
    [MySampleAsyncActionFilter(name: "Async9 Filter")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Testing of Filters
        /// </summary>
        /// <returns><see cref="List{T}"/> where T is <see cref="WeatherForecast"/></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        [MySampleActionFilter("GetMethod", -1)]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("Method Called.");
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