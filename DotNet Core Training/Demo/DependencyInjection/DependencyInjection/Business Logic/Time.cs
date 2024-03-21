using DependencyInjection.Interface;

namespace DependencyInjection.Business_Logic
{
    /// <summary>
    /// For understanding the lifetime of 3 different services.
    /// </summary>
    public class Time : IDateTime
    {
        /// <summary>
        /// Stores the current time.
        /// </summary>
        private readonly string _time;

        /// <summary>
        /// Initializes the fields and properties of <see cref="Time"/>
        /// </summary>
        public Time()
        {
            _time = DateTime.Now.ToString();
        }

        /// <summary>
        /// Returns the time fields of <see cref="Time"/>
        /// </summary>
        /// <returns>Time stores in _time Field.</returns>
        public string GetDate()
        {
            return _time;
        }
    }
}
