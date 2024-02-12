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
            string line = Environment.NewLine;

            string _errorMsg = exception.GetType().Name.ToString();
            string _exType = exception.GetType().ToString();

            try
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string filePath = directoryPath + "\\" + DateTime.Today.ToString("dd-MM-yy") + ".txt";

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