using System;
using System.IO;

namespace OnlineShoppingAPI.Business_Logic
{
    /// <summary>
    /// Class provides methods to write exceptions in txt file.
    /// </summary>
    internal class BLException
    {
        /// <summary>
        /// Write exception information to file.
        /// </summary>
        /// <param name="exception">Exception which occured</param>
        /// <param name="directoryPath">Log directory path</param>
        internal static void SendErrorToTxt(Exception exception, string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, $"{DateTime.Today:dd-MM-yy}.txt");

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }

                string line = Environment.NewLine;
                string _errorMsg = exception.GetType().Name;
                string _exType = exception.GetType().ToString();

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    // Error message creation
                    string error = $"Time :- {DateTime.Now:HH:mm:ss}{line}" +
                                   $"Error Message :- {_errorMsg}{line}" +
                                   $"Exception Type :- {_exType}{line}" +
                                   $"Error Stack Trace :- {exception.StackTrace}{line}";

                    writer.WriteLine(error);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                // Log the exception, e.g., print to console or use a dedicated logging framework
                Console.WriteLine($"An error occurred while logging: {ex}");
            }
        }
    }
}