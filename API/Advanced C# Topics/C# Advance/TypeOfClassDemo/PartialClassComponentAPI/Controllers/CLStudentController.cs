using PartialClassComponentAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PartialClassComponentAPI.Controllers
{
    /// <summary>
    /// Student model controller for creating and adding student data.
    /// </summary>
    public partial class CLStudentController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Stores the student data
        /// </summary>
        private static List<STU01> studentList;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize studentList one time
        /// </summary>
        static CLStudentController()
        {
            studentList = new List<STU01>();
        }

        #endregion
    }
}
