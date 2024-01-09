using APILeraningModel.Data;
using APILeraningModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace APILeraningModel.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController
    {
        private DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public Task<ActionResult<List<Category>>> GetAction([FromServices] DataContext context)
        {

        }
    }
}
