using GenericClassUsingDIAPI.Interface;
using GenericClassUsingDIAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace GenericClassUsingDIAPI.Business_Logic
{
    /// <summary>
    /// BLStudent class implementing IService interface for STU01 type
    /// </summary>
    public class BLStudent : IService<STU01>
    {
        #region Private Fields

        /// <summary>
        /// Static list to simulate a data store for student entities
        /// </summary>
        private static List<STU01> lstStudent = new List<STU01>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to delete a student entity by ID
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns></returns>
        public string Delete(int id)
        {
            // Find the student entity with the specified ID
            STU01 objStudent = lstStudent.FirstOrDefault(x => x.U01F01 == id);

            // Check if the student entity exists
            if (objStudent == null)
                return "No user Exists"; // Return a message if the student does not exist

            // Remove the student entity from the list
            lstStudent.Remove(objStudent);

            // Return success message
            return "Student Delete Successfully.";
        }

        /// <summary>
        /// Method to retrieve all student entities
        /// </summary>
        /// <returns>All Students</returns>
        public List<STU01> GetAllData()
        {
            return lstStudent; // Return the list of all student entities
        }

        /// <summary>
        /// Method to retrieve a student entity by ID
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student specific to id</returns>
        public STU01 GetById(int id)
        {
            return lstStudent.FirstOrDefault(x => x.U01F01 == id); // Return the student entity with the specified ID
        }

        /// <summary>
        /// Method to insert a new student entity
        /// </summary>
        /// <param name="entity">Student</param>
        /// <returns>Response message accoring to user data avaible or not</returns>
        public string Insert(STU01 entity)
        {
            // Check if a student with the same ID already exists
            STU01 objStudent = lstStudent.Find(x => x.U01F01 == entity.U01F01);

            // If the student does not exist, add it to the list
            if (objStudent == null)
            {
                lstStudent.Add(entity);
                return "Student Added Successfully";
            }

            // Return a message if the student already exists
            return "Student Already Exists.";
        }

        /// <summary>
        /// Method to update an existing student entity
        /// </summary>
        /// <param name="entity">Student updated data</param>
        /// <returns>Response message</returns>
        public string Update(STU01 entity)
        {
            // Find the student entity with the specified ID
            STU01 objStudent = lstStudent.Find(x => x.U01F01 == entity.U01F01);

            // If the student exists, update its information
            if (objStudent == null)
            {
                objStudent = entity;
                return "Student updated successfully";
            }

            // Return a message if the student does not exist
            return "Student not exists.";
        }

        #endregion
    }
}
