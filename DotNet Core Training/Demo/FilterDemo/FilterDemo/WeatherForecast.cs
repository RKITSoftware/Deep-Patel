namespace FilterDemo
{
    /// <summary>
    /// created by visual studio for demonstrating the controller example.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Weather time for specific day
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature in Celsius that Date.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature in Farenheit
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Summary according to Weather
        /// </summary>
        public string? Summary { get; set; }
    }
}