using SchoolManagementAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Business Logic class for managing student-related operations.
    /// </summary>
    public class BLStudentV1
    {
        /// <summary>
        /// Creates a new student and user, and adds them to the respective lists.
        /// </summary>
        /// <param name="objSTUUSR">The combined object containing student and user information.</param>
        /// <returns>HttpResponseMessage indicating the success or failure of the operation.</returns>
        internal HttpResponseMessage Create(STUUSR objSTUUSR)
        {
            try
            {
                if (objSTUUSR == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Data is null.");
                }

                // Assign unique IDs and set user role
                objSTUUSR.objStudent.U01F01 = BLHelper.studentID + 1;
                objSTUUSR.objStudent.U01F06 = objSTUUSR.objUser.R01F02;

                objSTUUSR.objUser.R01F01 = BLHelper.userID + 1;
                objSTUUSR.objUser.R01F04 = "Student";

                // Increment ID counters
                BLHelper.studentID++;
                BLHelper.userID++;

                // Add student and user to the respective lists
                BLHelper.lstStudent.Add(objSTUUSR.objStudent);
                BLHelper.lstUsers.Add(objSTUUSR.objUser);

                return BLHelper.ResponseMessage(HttpStatusCode.Created,
                    "Student Created Successfully");
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during creating student.");
            }
        }

        /// <summary>
        /// Deletes a student and associated user based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>HttpResponseMessage indicating the success or failure of the operation.</returns>
        internal HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Id can't be negative nor zero.");
                }

                // Find the existing student based on ID
                STU01 existingStudent = BLHelper.lstStudent.FirstOrDefault(stu => stu.U01F01 == id);

                if (existingStudent == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                        "Student can't be found.");
                }

                // Remove student from the list and associated user from the user list
                BLHelper.lstStudent.Remove(existingStudent);
                BLHelper.lstUsers.RemoveAll(usr => usr.R01F02.Equals(existingStudent.U01F06));

                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Student deleted successfully.");
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during deleting student.");
            }
        }

        /// <summary>
        /// Retrieves a student based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the student to be retrieved.</param>
        /// <returns>The student matching the provided ID or null if not found.</returns>
        internal STU01 Get(int id)
        {
            try
            {
                return BLHelper.lstStudent.FirstOrDefault(stu => stu.U01F01 == id);
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>An object containing the list of all students.</returns>
        internal object GetAll()
        {
            try
            {
                return BLHelper.lstStudent;
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates information for an existing student.
        /// </summary>
        /// <param name="objStudent">The updated student information.</param>
        /// <returns>HttpResponseMessage indicating the success or failure of the operation.</returns>
        internal HttpResponseMessage Update(STU01 objStudent)
        {
            try
            {
                if (objStudent == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Student object can't be null.");
                }

                // Find the existing student based on ID and username
                STU01 existingStudent = BLHelper.lstStudent.FirstOrDefault(stu =>
                    stu.U01F01 == objStudent.U01F01
                        && stu.U01F06.Equals(objStudent.U01F06));

                if (existingStudent == null)
                    return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                        "Student doesn't exist");

                // Update student information
                existingStudent.U01F02 = objStudent.U01F02;
                existingStudent.U01F03 = objStudent.U01F03;
                existingStudent.U01F04 = objStudent.U01F04;

                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Student updated successfully.");
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during updating student.");
            }
        }
    }
}
