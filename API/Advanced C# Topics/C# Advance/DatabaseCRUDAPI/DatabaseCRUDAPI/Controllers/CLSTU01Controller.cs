using DatabaseCRUDAPI.Business_Logic;
using DatabaseCRUDAPI.Models.DTO;
using DatabaseCRUDAPI.Models.Enums;
using System.Web.Http;

namespace DatabaseCRUDAPI.Controllers
{
    /// <summary>
    /// Controller for CRUD operations related to student data.
    /// </summary>
    [RoutePrefix("api/CLStudent")]
    public class CLSTU01Controller : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Insance of <see cref="BLSTU01"/>
        /// </summary>
        private BLSTU01 _bLSTU01;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CLSTU01Controller class.
        /// </summary>
        public CLSTU01Controller()
        {
            _bLSTU01 = new BLSTU01();
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Creates a new student record.
        /// </summary>
        /// <param name="objSTU01DTO">Student DTO containing data to be created.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateStudent(DTOSTU01 objSTU01DTO)
        {
            _bLSTU01.PreSave(objSTU01DTO, EnmOperation.Create);

            if (!_bLSTU01.Validation())
                return Ok("Details are incorrect.");

            return Ok(_bLSTU01.Save(EnmOperation.Create));
        }

        /// <summary>
        /// Updates an existing student record.
        /// </summary>
        /// <param name="objSTU01DTO">Student DTO containing data to be updated.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public IHttpActionResult UpdateData(DTOSTU01 objSTU01DTO)
        {
            _bLSTU01.PreSave(objSTU01DTO, EnmOperation.Update);

            if (!_bLSTU01.Validation())
                return Ok("Validation fail.");

            return Ok(_bLSTU01.Save(EnmOperation.Update));
        }

        /// <summary>
        /// Retrieves all students data.
        /// </summary>
        /// <returns>HTTP response containing students data.</returns>
        [HttpGet]
        [Route("GetStudentsData")]
        public IHttpActionResult GetStudentsData() => Ok(_bLSTU01.ReadData());

        /// <summary>
        /// Deletes a student record by ID.
        /// </summary>
        /// <param name="id">ID of the student to be deleted.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IHttpActionResult DeleteData(int id) => Ok(_bLSTU01.Delete(id));

        #endregion
    }
}
