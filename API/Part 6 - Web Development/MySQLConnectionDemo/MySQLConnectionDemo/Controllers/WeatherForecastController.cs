using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MySQLConnectionDemo.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public JsonResult Get()
        {
            try
            {
                MySqlConnection con;
                MySqlCommand cmd;
                MySqlDataReader dr;

                string path = "server=localhost;uid=root;pwd=@Deep2513;database=college";
                con = new MySqlConnection();
                con.ConnectionString = path;
                con.Open();

                string sql = "select * from city";
                //string sql = "insert into city values (2, \"Delhi\", 22)";
                cmd = new MySqlCommand(sql, con);

                Console.WriteLine("Herllo");
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    var obj = dr["city"];
                    Console.WriteLine(dr["city"]);
                }

                return new JsonResult(Ok());
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new JsonResult(NotFound());
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
