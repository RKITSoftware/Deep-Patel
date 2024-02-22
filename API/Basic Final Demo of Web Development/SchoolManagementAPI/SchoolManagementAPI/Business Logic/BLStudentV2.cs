using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Business Logic class for managing version 2 of student-related operations.
    /// </summary>
    public class BLStudentV2
    {
        /// <summary>
        /// List to store instances of version 2 student objects.
        /// </summary>
        public static List<STU02> lstStudentV2;

        /// <summary>
        /// Static constructor to initialize the list when the class is first accessed.
        /// </summary>
        static BLStudentV2()
        {
            lstStudentV2 = new List<STU02>();
        }

        /// <summary>
        /// Retrieves all version 2 student objects.
        /// </summary>
        /// <returns>A list containing all version 2 student objects.</returns>
        public List<STU02> GetAll()
        {
            return lstStudentV2;
        }

        /// <summary>
        /// Adds a version 2 student object to the list.
        /// </summary>
        /// <param name="objStudent">The version 2 student object to be added.</param>
        /// <returns>HttpResponseMessage indicating the success (HTTP 201 - Created) of the operation.</returns>
        internal HttpResponseMessage Add(STU02 objStudent)
        {
            lstStudentV2.Add(objStudent);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
