﻿using Newtonsoft.Json;
using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing student-related operations.
    /// </summary>
    [RoutePrefix("student")] // All user routes starts with student
    [AuthenticationAttribute] // Apply authentication filter to ensure only authenticated users can access the controller.
    // [CacheFilter(TimeDuration = 100)] // Apply caching filter to cache responses for a specified duration.
    public class CLStudentController : ApiController
    {
        #region API Endpoints

        /// <summary>
        /// Create a student
        /// </summary>
        /// <param name="student">Get student model from api body</param>
        /// <returns>200 Response if user created</returns>
        [HttpPost]
        [Route("add")] // POST Endpoint :- student/add
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to add a student.
        public IHttpActionResult AddStudent([FromBody] STU01 student)
        {
            return Ok(BLStudent.AddData(student));
        }

        /// <summary>
        /// Write the student data to the studentData.json file
        /// </summary>
        /// <returns>Ok Response for data successfully written.</returns>
        [HttpPost]
        [Route("update")] // POST Endpoint  :- student/update
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to update student data.
        public IHttpActionResult WriteStudentDataToFile()
        {
            BLStudent.UpdateStudentDataFile();
            return Ok("Data Written Successfully.");
        }

        /// <summary>
        /// Get all Student Data
        /// </summary>
        /// <returns>All Student Data</returns>
        [HttpGet]
        [Route("get/allData")] // GET Endpoint :- student/get/allData
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to get all student data.
        public IHttpActionResult GetAllStudentData()
        {
            // Return the list of all students.
            return Ok(BLStudent.GetAllStudentData());
        }

        /// <summary>
        /// Retrieve Student by using student Id
        /// </summary>
        /// <param name="studentId">For Retrieving Student Data</param>
        /// <returns>Student Data</returns>
        [HttpGet]
        [Route("get/{studentId}")] // Get Endpoint :- student/get/1
        [AuthorizationAttribute(Roles = "Student")] // Allow only users with the "Student" role to get individual student data.
        public IHttpActionResult GetStudentData(int studentId)
        {
            // Retrieve a student by ID and return it.
            STU01 student = BLStudent.GetStudentById(studentId);

            if (student == null)
                return NotFound();

            // Return student with Ok Response
            return Ok(student);
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="delId">For find ftudent</param>
        /// <returns>200 Response With student delete message</returns>
        [HttpDelete]
        [Route("delete/{delId}")] // Delete Endpoint :- student/delete/1
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to delete a student.
        public IHttpActionResult DeleteStudentData(int delId)
        {
            BLStudent.DeleteStudent(delId);
            return Ok("Student Deleted");
        }

        /// <summary>
        /// Update the student's data with new data
        /// </summary>
        /// <param name="studentId">Student id for finding specific student</param>
        /// <param name="updateData">New updated student data</param>
        /// <returns>Updated data</returns>
        [HttpPut]
        [Route("put/studentData/{studentId}")] // Put Endpoint :- student/put/studentData/1
        [AuthorizationAttribute(Roles = "Student")] // Allow only users with the "Student" role to update their own data.
        public IHttpActionResult UpdateStudentInfo(int studentId, [FromBody] STU01 updateData)
        {
            return Ok(BLStudent.UpdateStudentData(studentId, updateData));
        }

        #endregion
    }
}