using Newtonsoft.Json;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for managing student-related operations.
    /// </summary>
    public class BLStudent
    {
        #region Private Fields

        /// <summary>
        /// File location of studentData.json
        /// </summary>
        private static readonly string filePath = HttpContext.Current.Server.MapPath("/Data/studentData.json");

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

        /// <summary>
        /// Static constructor to initialize static fields when the class is first accessed.
        /// </summary>
        static BLStudent()
        {
            // Initialize studentList and noOfNextStudentId when the controller is first accessed.
            string jsonContent = File.ReadAllText(filePath);

            lstStudent = JsonConvert.DeserializeObject<List<STU01>>(jsonContent);
            noOfNextStudentId = lstStudent.OrderByDescending(stu => stu.U01F01).FirstOrDefault().U01F01;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new student to the list and updates associated user information.
        /// </summary>
        /// <param name="objSTU01">Student object to be added.</param>
        /// <returns>Success message upon student creation.</returns>
        public string AddData(STU01 objSTU01)
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

        /// <summary>
        /// Updates the student data file with the current student list.
        /// </summary>
        public void UpdateStudentDataFile()
        {
            // Serialize the student list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(lstStudent, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }

        /// <summary>
        /// Retrieves the list of all students.
        /// </summary>
        /// <returns>List of all students.</returns>
        public List<STU01> GetAllStudentData()
        {
            return lstStudent;
        }

        /// <summary>
        /// Retrieves a specific student by their ID.
        /// </summary>
        /// <param name="id">Student ID.</param>
        /// <returns>Student object corresponding to the provided ID.</returns>
        public STU01 GetStudentById(int id)
        {
            return lstStudent.FirstOrDefault(stu => stu.U01F01 == id);
        }

        /// <summary>
        /// Deletes a student along with their associated user information.
        /// </summary>
        /// <param name="delId">ID of the student to be deleted.</param>
        public void DeleteStudent(int delId)
        {
            STU01 objStudent = lstStudent.Find(stu => stu.U01F01 == delId);
            USR01 objUser = BLUser.lstUser
                .Find(usr => usr.R01F02.Equals(objStudent.U01F03.Split('@')[0]));

            BLUser.lstUser.Remove(objUser);
            lstStudent.Remove(objStudent);
        }

        /// <summary>
        /// Updates the data of an existing student.
        /// </summary>
        /// <param name="studentId">ID of the student to be updated.</param>
        /// <param name="objNewStudentData">New student data.</param>
        /// <returns>Success message upon data update.</returns>
        public string UpdateStudentData(STU01 objNewStudentData)
        {
            STU01 objStudent = lstStudent.Find(stu => stu.U01F01 == objNewStudentData.U01F01);

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
