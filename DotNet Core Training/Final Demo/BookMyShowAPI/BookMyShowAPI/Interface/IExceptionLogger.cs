namespace BookMyShowAPI.Interface
{
    public interface IExceptionLogger
    {
        /// <summary>
        /// Logs the exception to the file.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> to log.</param>
        void Log(Exception exception);
    }
}
