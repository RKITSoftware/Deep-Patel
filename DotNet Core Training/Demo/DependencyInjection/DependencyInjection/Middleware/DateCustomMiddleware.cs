using DependencyInjection.Interface;

namespace DependencyInjection.Middleware
{
    /// <summary>
    /// Custom middleware for understanding the lifetime or services.
    /// </summary>
    public class DateCustomMiddleware : IMiddleware
    {
        // Store the time that middleware is executing.
        public const string ContextItemsKey = nameof(ContextItemsKey);

        /// <summary>
        /// Logger for logging information of this <see cref="DateCustomMiddleware"/>
        /// </summary>
        private readonly ILogger<DateCustomMiddleware> _logger;

        /// <summary>
        /// Stored the instance of <see cref="IDateTime"/> interface for <see cref="DateCustomMiddleware"/>.
        /// </summary>
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initialize the fields and properites of <see cref="DateCustomMiddleware"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dateTime"></param>
        public DateCustomMiddleware(ILogger<DateCustomMiddleware> logger, IDateTime dateTime)
        {
            _logger = logger;
            _dateTime = dateTime;
        }

        /// <summary>
        /// Request handling method for <see cref="DateCustomMiddleware"/>
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> for the current request.</param>
        /// <param name="next"><see cref="RequestDelegate"/> for executing the next middlware pipeline.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);

            var date = _dateTime.GetDate();

            context.Items.Add(ContextItemsKey, date);

            var logMessage = $"Middleware: The Date from Time is {date}";

            _logger.LogInformation(logMessage);

            await next(context);
        }
    }
}
