using GenericClassUsingDIAPI.Interface;
using GenericClassUsingDIAPI.Models;
using System.Web.Http;

namespace GenericClassUsingDIAPI.Controllers
{
    /// <summary>
    /// Controller class for handling student-related CRUD operations
    /// </summary>
    public class CLStudentController : CLGenericController<STU01>
    {
        #region Constructors

        /// <summary>
        /// Constructor to initialize the controller with a service for STU01
        /// </summary>
        /// <param name="service">Interface reference</param>
        public CLStudentController(IService<STU01> service) : base(service) { }

        #endregion

        #region API Endpoints

        /// <summary>
        /// Override the base class method to provide a specific implementation for retrieving all student data
        /// </summary>
        /// <returns>All data of Student</returns>
        [HttpGet]
        public override IHttpActionResult GetAllData()
        {
            return base.GetAllData(); // Call the base class method to retrieve all student data
        }

        #endregion
    }
}
