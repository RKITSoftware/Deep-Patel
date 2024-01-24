using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MySQLConnectionDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region Private Fields

        // Array of weather summaries for demonstration purposes.
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        #endregion

        #region Constructor

        // Constructor to initialize the controller with a logger.
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        #endregion

        /// <summary>
        /// HTTP GET endpoint for retrieving weather forecast data.
        /// </summary>
        /// <returns>Weather Data</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public JsonResult Get()
        {
            try
            {
                // Connection string for connecting to MySQL database.
                string path = "server=localhost;uid=root;pwd=@Deep2513;database=college";

                // Create a MySqlConnection object and open the connection.
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = path;
                con.Open();

                // SQL query to retrieve data from the "city" table.
                string sql = "select * from city";
                //string sql = "insert into city values (2, \"Delhi\", 22)";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Execute the query and read the data using a MySqlDataReader.
                MySqlDataReader dataReader = cmd.ExecuteReader();

                // Check if there is data to read.
                if (dataReader.Read())
                {
                    // Access and print the "city" column data.
                    var obj = dataReader["city"];
                    Console.WriteLine(dataReader["city"]);
                }

                // Return a JsonResult with an "Ok" response.
                return new JsonResult(Ok());
            }
            catch (MySqlException ex)
            {
                // Handle exceptions and log error details.
                Console.WriteLine(ex.ToString());
            }

            // Return a JsonResult with a "NoContent" response in case of an exception.
            return new JsonResult(NoContent());
        }
    }
}
