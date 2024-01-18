using Newtonsoft.Json;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace SchoolManagementAPI.Business_Logic
{
    public class BLUser
    {
        #region Private Fields

        /// <summary>
        /// File location of studentData.json
        /// </summary>
        private readonly static string filePath = "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\userData.json";

        /// <summary>
        /// Student List for Operation of API Endpoints
        /// </summary>
        public static List<USR01> lstUser;

        /// <summary>
        /// Student id for creating new student's studentId. 
        /// </summary>
        public static int noOfNextUserId { get; set; }

        #endregion

        #region Constructor

        public BLUser() { }

        static BLUser()
        {
            // Read user data from the JSON file when the controller is initialized.
            string jsonContent = File.ReadAllText(filePath);

            lstUser = JsonConvert.DeserializeObject<List<USR01>>(jsonContent);
            noOfNextUserId = lstUser.OrderByDescending(e => e.R01F01).FirstOrDefault().R01F01; ;
        }

        #endregion

        #region Public Methods

        public static void AddUser(USR01 objUSR01)
        {
            objUSR01.R01F01 = ++noOfNextUserId;
            lstUser.Add(objUSR01);
        }

        public static List<USR01> GetUserList()
        {
            return lstUser;
        }

        public static USR01 GetUser(int userId)
        {
            return lstUser.Find(usr => usr.R01F01 == userId);
        }

        public static void UpdateFileData()
        {
            // Serialize the user list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(lstUser, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }

        public static string DeleteUser(int delUserId)
        {
            USR01 objUSR01 = GetUser(delUserId);

            if (objUSR01 == null)
                return "No user found";

            lstUser.Remove(objUSR01);
            return "User Deleted Successfully";
        }

        public static string UpdateUserData(int userId, USR01 objUpdateData)
        {
            USR01 objUSR01 = lstUser.Find(usr => usr.R01F01 == userId);

            if (objUSR01 == null)
                return "No user found";

            objUSR01.R01F03 = objUpdateData.R01F03;
            return "User updated Successfully";
        }

        #endregion
    }
}