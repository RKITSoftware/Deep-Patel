using DependencyInjection.Interface;
using DependencyInjection.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopeController : ControllerBase
    {
        private readonly IDateTime _dateTime;

        public ScopeController(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public string DateFromDependency { get; set; }
        public string DateFromMiddleWare { get; set; }

        [HttpGet("")]
        public ActionResult<object> Get()
        {
            if (HttpContext.Items.TryGetValue(DateCustomMiddleware.ContextItemsKey, out var mwDate)
                && mwDate is string contextItemsKey)
            {
                DateFromMiddleWare = contextItemsKey;
            }

            var date = _dateTime.GetDate();
            DateFromDependency = date;

            return Ok(new { dateFromDependency = DateFromDependency, dateFromMiddleware = DateFromMiddleWare });
        }
    }
}
