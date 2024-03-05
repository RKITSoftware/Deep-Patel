using Newtonsoft.Json;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for managing user-related operations.
    /// </summary>
    public class BLUser
    {
        #region Private Fields

        /// <summary>
        /// File location of userData.json
        /// </summary>
        private readonly static string filePath = "F:\\Deep - 380\\Training\\API" +
            "\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI" +
            "\\Data\\userData.json";

        /// <summary>
        /// List of users for API endpoints.
        /// </summary>
        public static List<USR01> lstUser;

        /// <summary>
        /// User id for creating new user's user id.
        /// </summary>
        public static int noOfNextUserId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for BLUser.
        /// </summary>
        public BLUser() { }

        /// <summary>
        /// Static constructor to initialize static fields when the class is first accessed.
        /// </summary>
        static BLUser()
        {
            // Read user data from the JSON file when the controller is initialized.
            string jsonContent = File.ReadAllText(filePath);
            lstUser = JsonConvert.DeserializeObject<List<USR01>>(jsonContent);

            noOfNextUserId = lstUser.OrderByDescending(e => e.R01F01).FirstOrDefault().R01F01; ;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new user to the user list.
        /// </summary>
        /// <param name="objUSR01">User object to be added.</param>
        public static void AddUser(USR01 objUSR01)
        {
            objUSR01.R01F01 = ++noOfNextUserId;
            lstUser.Add(objUSR01);
        }

        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>List of all users.</returns>
        public static List<USR01> GetUserList()
        {
            return lstUser;
        }

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User object corresponding to the provided ID.</returns>
        public static USR01 GetUser(int userId)
        {
            return lstUser.Find(usr => usr.R01F01 == userId);
        }

        /// <summary>
        /// Updates the user data file with the current user list.
        /// </summary>
        public static void UpdateFileData()
        {
            // Serialize the user list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(lstUser, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="delUserId">ID of the user to be deleted.</param>
        /// <returns>Success message upon user deletion.</returns>
        public static string DeleteUser(int delUserId)
        {
            USR01 objUSR01 = GetUser(delUserId);

            if (objUSR01 == null)
                return "No user found";

            lstUser.Remove(objUSR01);
            return "User Deleted Successfully";
        }

        /// <summary>
        /// Updates the data of an existing user.
        /// </summary>
        /// <param name="objUpdateData">New user data.</param>
        /// <returns>Success message upon user data update.</returns>
        public static string UpdateUserData(USR01 objUpdateData)
        {
            USR01 objUSR01 = lstUser.Find(usr => usr.R01F01 == objUpdateData.R01F01);

            if (objUSR01 == null)
                return "No user found";

            objUSR01.R01F03 = objUpdateData.R01F03;
            return "User updated Successfully";
        }

        #endregion
    }
}
