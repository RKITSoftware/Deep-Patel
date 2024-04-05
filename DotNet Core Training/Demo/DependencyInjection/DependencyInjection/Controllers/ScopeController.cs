using DependencyInjection.Interface;
using DependencyInjection.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// <see cref="ScopeController"/> for demonstrating the scope of service lifetime.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScopeController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IDateTime"/> for getting time details.
        /// </summary>
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initialize the <see cref="ScopeController"/> fields and proeprties.
        /// </summary>
        /// <param name="dateTime"><see cref="IDateTime"/> instance</param>
        public ScopeController(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        /// <summary>
        /// Store the time from the dependency.
        /// </summary>
        public string DateFromDependency { get; set; }

        /// <summary>
        /// Stores the time which is return by Middleware.
        /// </summary>
        public string DateFromMiddleWare { get; set; }

        /// <summary>
        /// Show the time of dependency and middleware and difference between them.
        /// </summary>
        /// <returns>Object which contains <see cref="DateFromDependency"/> and <see cref="DateFromMiddleWare"/></returns>
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
