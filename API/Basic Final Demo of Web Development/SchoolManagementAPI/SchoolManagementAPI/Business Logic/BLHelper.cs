using Newtonsoft.Json;
using SchoolManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Helper class containing utility methods and static data for the School Management API.
    /// </summary>
    public class BLHelper
    {
        /// <summary>
        /// Stores the file path where log information of exceptions is stored.
        /// </summary>
        private static readonly string _logFolderPath =
            HttpContext.Current.Application["LogFolderPath"] as string;

        /// <summary>
        /// Store file path of Admin Data.json file
        /// </summary>
        private static readonly string _adminFilePath =
            HttpContext.Current.Application["AdminData"] as string;

        /// <summary>
        /// Store file path of Student Data.json file
        /// </summary>
        private static readonly string _studentFilePath =
            HttpContext.Current.Application["StudentData"] as string;

        /// <summary>
        /// Store file path of User Data.json file
        /// </summary>
        private static readonly string _userFilePath =
            HttpContext.Current.Application["UserData"] as string;

        /// <summary>
        /// Cache for storing server-related data.
        /// </summary>
        public static Cache ServerCache;

        /// <summary>
        /// Store all users data
        /// </summary>
        public static List<USR01> lstUsers;

        /// <summary>
        /// Store all Student's data
        /// </summary>
        public static List<STU01> lstStudent;

        /// <summary>
        /// Store all Admin's Data
        /// </summary>
        public static List<ADM01> lstAdmin;

        /// <summary>
        /// Auto increment userId for user data.
        /// </summary>
        public static int userID;

        /// <summary>
        /// Auto increment studentID for student data.
        /// </summary>
        public static int studentID;

        /// <summary>
        /// Auto increment adminID for admin data.
        /// </summary>
        public static int adminID;

        /// <summary>
        /// Static constructor to initialize static lists and ID counters from JSON files.
        /// </summary>
        static BLHelper()
        {
            ServerCache = new Cache();

            // Deserialize JSON files into lists
            lstUsers = JsonConvert.DeserializeObject<List<USR01>>(
                File.ReadAllText(_userFilePath));

            lstStudent = JsonConvert.DeserializeObject<List<STU01>>(
                File.ReadAllText(_studentFilePath));

            lstAdmin = JsonConvert.DeserializeObject<List<ADM01>>(
                File.ReadAllText(_adminFilePath));

            // Set ID counters based on the last IDs in each list
            userID = (lstUsers.Count > 0) ? lstUsers[lstUsers.Count - 1].R01F01 : 0;
            adminID = (lstAdmin.Count > 0) ? lstAdmin[lstAdmin.Count - 1].M01F01 : 0;
            studentID = (lstStudent.Count > 0) ? lstStudent[lstStudent.Count - 1].U01F01 : 0;
        }

        /// <summary>
        /// Writes exception information to a text file.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="directoryPath">The directory path for storing log files.</param>
        public static void LogError(Exception exception)
        {
            try
            {
                if (!Directory.Exists(_logFolderPath))
                {
                    Directory.CreateDirectory(_logFolderPath);
                }

                string filePath = Path.Combine(_logFolderPath, $"{DateTime.Today:dd-MM-yy}.txt");

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
                    string error = $"Time: {DateTime.Now:HH:mm:ss}{line}" +
                                   $"Error Message: {_errorMsg}{line}" +
                                   $"Exception Type: {_exType}{line}" +
                                   $"Error Stack Trace: {exception.StackTrace}{line}";

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

        /// <summary>
        /// Creates an HttpResponseMessage with the specified HTTP status code and message content.
        /// </summary>
        /// <param name="statusCode">The HTTP status code for the response.</param>
        /// <param name="message">The content message to be included in the response.</param>
        /// <returns>An HttpResponseMessage with the specified status code and message content.</returns>
        public static HttpResponseMessage ResponseMessage(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            };
        }

        /// <summary>
        /// Updates the JSON files with the current data in the lists.
        /// </summary>
        public static void FileUpdate()
        {
            try
            {
                // Serialize and write lists to JSON files
                File.WriteAllText(_userFilePath,
                    JsonConvert.SerializeObject(lstUsers, Formatting.Indented));

                File.WriteAllText(_adminFilePath,
                    JsonConvert.SerializeObject(lstAdmin, Formatting.Indented));

                File.WriteAllText(_studentFilePath,
                    JsonConvert.SerializeObject(lstStudent, Formatting.Indented));
            }
            catch (Exception ex)
            {
                // Log the exception if an error occurs during file update
                LogError(ex);
            }
        }

        /// <summary>
        /// Retrieves a user by username and password.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>The user matching the provided credentials or null if not found.</returns>
        internal static USR01 GetUser(string username, string password)
        {
            return lstUsers.FirstOrDefault(user =>
                        user.R01F02.Equals(username) &&
                        user.R01F03.Equals(password));
        }

        /// <summary>
        /// Checks if a user with the provided username and password exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>True if a user with the provided credentials exists, false otherwise.</returns>
        internal static bool IsExist(string username, string password)
        {
            return lstUsers.Any(user =>
                        user.R01F02.Equals(username) &&
                        user.R01F03.Equals(password));
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="v">The ID of the user to retrieve.</param>
        /// <returns>The user matching the provided ID or null if not found.</returns>
        internal static USR01 GetUser(int v)
        {
            return lstUsers.FirstOrDefault(user => user.R01F01 == v);
        }
    }
}
