namespace MySQLConnectionDemo
{
    /// <summary>
    /// WeatherForecast class represents a model for weather forecast data.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Property to hold the date of the weather forecast.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Property to hold the temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Property to calculate the temperature in Fahrenheit based on the Celsius temperature.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Property to hold a summary of the weather forecast (nullable).
        /// </summary>
        public string? Summary { get; set; }
    }
}
