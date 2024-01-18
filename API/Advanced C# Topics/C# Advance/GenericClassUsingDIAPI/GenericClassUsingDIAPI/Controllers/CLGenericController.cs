using GenericClassUsingDIAPI.Interface;
using System.Web.Http;

namespace GenericClassUsingDIAPI.Controllers
{
    /// <summary>
    /// Generic Web API controller class for CRUD operations on a generic type T
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public class CLGenericController<T> : ApiController
        where T : class
    {
        #region Public Fields

        // Readonly field to store the service implementing IService<T>
        public readonly IService<T> _service;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the controller with a service
        /// </summary>
        /// <param name="service">IService interface reference</param>
        public CLGenericController(IService<T> service)
        {
            _service = service;
        }

        #endregion

        #region API Endpoints

        /// <summary>
        /// HTTP GET method to retrieve all data of type T
        /// </summary>
        /// <returns>All data</returns>
        [HttpGet]
        public virtual IHttpActionResult GetAllData()
        {
            return Ok(_service.GetAllData()); // Return the result of GetAllData method from the service
        }

        /// <summary>
        /// HTTP GET method to retrieve data of type T by ID
        /// </summary>
        /// <param name="id">Find data using id</param>
        /// <returns>Data according to id</returns>
        [HttpGet]
        public IHttpActionResult GetDataById(int id)
        {
            return Ok(_service.GetById(id)); // Return the result of GetById method from the service
        }

        /// <summary>
        /// HTTP POST method to add new data of type T
        /// </summary>
        /// <param name="entity">Data to add</param>
        /// <returns>Result of insert method</returns>
        [HttpPost]
        public IHttpActionResult AddData([FromBody] T entity)
        {
            return Ok(_service.Insert(entity)); // Return the result of Insert method from the service
        }

        /// <summary>
        /// HTTP PUT method to update existing data of type T
        /// </summary>
        /// <param name="entity">Data to Update</param>
        /// <returns>Result of Update Method</returns>
        [HttpPut]
        public IHttpActionResult PutData([FromBody] T entity)
        {
            return Ok(_service.Update(entity)); // Return the result of Update method from the service
        }

        /// <summary>
        /// HTTP DELETE method to delete data of type T by ID
        /// </summary>
        /// <param name="id">Delete id for deleting data</param>
        /// <returns>Response of Delete Method</returns>
        [HttpDelete]
        public IHttpActionResult DeleteData(int id)
        {
            return Ok(_service.Delete(id)); // Return the result of Delete method from the service
        }

        #endregion
    }
}
