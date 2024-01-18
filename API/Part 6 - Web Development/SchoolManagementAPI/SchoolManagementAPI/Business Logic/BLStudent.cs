using Newtonsoft.Json;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementAPI.Business_Logic
{
    public class BLStudent
    {
        #region Private Fields

        /// <summary>
        /// File location of studentData.json
        /// </summary>
        private readonly static string filePath = "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\studentData.json";

        /// <summary>
        /// Student List for Operation of API Endpoints
        /// </summary>
        private static List<STU01> lstStudent;

        /// <summary>
        /// Student id for creating new student's studentId. 
        /// </summary>
        public static int noOfNextStudentId { get; set; }

        #endregion

        #region Constructor

        public BLStudent() { }

        static BLStudent()
        {
            // Initialize studentList and noOfNextStudentId when the controller is first accessed.
            string jsonContent = File.ReadAllText(filePath);

            lstStudent = JsonConvert.DeserializeObject<List<STU01>>(jsonContent);
            noOfNextStudentId = lstStudent.OrderByDescending(stu => stu.U01F01).FirstOrDefault().U01F01;
        }

        #endregion

        #region Public Methods

        public static string AddData(STU01 objSTU01)
        {
            objSTU01.U01F01 = ++noOfNextStudentId;
            lstStudent.Add(objSTU01);

            BLUser.AddUser(new USR01()
            {
                R01F01 = ++BLUser.noOfNextUserId,
                R01F02 = objSTU01.U01F03.Split('@')[0],
                R01F03 = objSTU01.U01F04,
                R01F04 = "Student"
            });

            return "Student Created Successfully";
        }

        public static void UpdateStudentDataFile()
        {
            // Serialize the student list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(lstStudent, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }

        public static List<STU01> GetAllStudentData()
        {
            return lstStudent;
        }

        public static STU01 GetStudentById(int id)
        {
            return lstStudent.FirstOrDefault(stu => stu.U01F01 == id);
        }

        public static void DeleteStudent(int delId)
        {
            STU01 objStudent = lstStudent.Find(stu => stu.U01F01 == delId);
            USR01 objUser = BLUser.lstUser
                .Find(usr => usr.R01F02.Equals(objStudent.U01F03.Split('@')[0]));

            BLUser.lstUser.Remove(objUser);
            lstStudent.Remove(objStudent);
        }

        public static string UpdateStudentData(int studentId, [FromBody] STU01 objNewStudentData)
        {
            STU01 objStudent = lstStudent.Find(stu => stu.U01F01 == studentId);

            if (objStudent == null)
                return "Not Found";
            
            objStudent.U01F02 = objNewStudentData.U01F02;
            objStudent.U01F05 = objNewStudentData.U01F05;
            objStudent.U01F06 = objNewStudentData.U01F06;
            objStudent.U01F07 = objNewStudentData.U01F07;

            return "Data Updated Successfully";
        }

        #endregion
    }
}