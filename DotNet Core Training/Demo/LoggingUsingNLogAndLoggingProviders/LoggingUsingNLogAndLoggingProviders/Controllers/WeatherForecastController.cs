using LoggingUsingNLogAndLoggingProviders.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LoggingUsingNLogAndLoggingProviders.Controllers
{
    /// <summary>
    /// Default WeatherForecastController which is created default by Visual Studio
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// Store the weather type.
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// For logging the infomration
        /// </summary>
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Logs the <see cref="Exception"/> generated during the process.
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// To initialize the objects of WeatherForecast
        /// </summary>
        /// <param name="logger">Information logger</param>
        /// <param name="exceptionLogger">Exception Logger</param>
        public WeatherForecastController(ILoggerManager logger, IExceptionLogger exceptionLogger)
        {
            _logger = logger;
            _exceptionLogger = exceptionLogger;
        }

        /// <summary>
        /// Get Method for demonstarting the logger using NLog.
        /// </summary>
        /// <returns><see cref="List{T}"/> where <see cref="{T}"/> is <see cref="WeatherForecast"/></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInfo("Get Method called of weather controller.");
            _logger.LogDebug("Get Method called of weather controller.");
            _logger.LogWarn("Get Method called of weather controller.");
            _logger.LogError("Get Method called of weather controller.");

            try
            {
                int a = 0;
                int b = 1 / a;

            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }

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