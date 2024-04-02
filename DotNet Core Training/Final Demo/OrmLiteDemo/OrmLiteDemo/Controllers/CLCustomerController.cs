using Microsoft.AspNetCore.Mvc;
using OrmLiteDemo.Model;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace OrmLiteDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLCustomerController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbFactory;

        public CLCustomerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                List<Customer> customers = db.Select<Customer>();
                return Ok(customers);
            }
        }
    }
}
