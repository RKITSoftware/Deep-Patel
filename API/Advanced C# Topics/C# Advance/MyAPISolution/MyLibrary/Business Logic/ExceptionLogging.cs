using System;
using System.IO;

namespace MyLibrary.Business_Logic
{
    /// <summary>
    /// Class provides methods to write exceptions in txt file.
    /// </summary>
    internal class ExceptionLogging
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static string  _errorMsg, _exType;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Write exception information to file.
        /// </summary>
        /// <param name="exception">Exception which occured</param>
        /// <param name="directoryPath">Log directory path</param>
        internal static void SendErrorToTxt(Exception exception, string directoryPath)
        {
            string line = Environment.NewLine;

            _errorMsg = exception.GetType().Name.ToString();
            _exType = exception.GetType().ToString();

            try
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string filePath = directoryPath + DateTime.Today.ToString("dd-MM-yy") + ".txt";

                if (!File.Exists(filePath))
                    File.Create(filePath).Dispose();

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    // Error message create
                    string error = "Time :- " + DateTime.Now.ToString("HH:mm:ss") + line +
                    "Error Message :- " + _errorMsg + line +
                    "Exception Type :- " + _exType + line +
                    "Error Stack Trace :- " + exception.StackTrace.ToString() + line;

                    writer.WriteLine(error);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}