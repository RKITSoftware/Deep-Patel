using System.Web.Http;
using VerificationDemo.BL.Master.Interface;
using VerificationDemo.BL.Master.Service;
using VerificationDemo.Models;
using VerificationDemo.Models.DTO;
using VerificationDemo.Models.Enum;

namespace VerificationDemo.Controllers
{
    /// <summary>
    /// Controller to handle HTTP request for the USR01 model.
    /// </summary>
    [RoutePrefix("api/CLUSR01")]
    public class CLUSR01Controller : ApiController
    {
        #region Private Fields

        /// <summary>
        /// USR01 model's services.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to initialize the instance of the controller.
        /// </summary>
        public CLUSR01Controller()
        {
            _usr01Service = new BLUSR01Handler();
        }

        #endregion

        #region API Endpoints

        /// <summary>
        /// Inserts a USR01 model record into the database.
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing the USR01 information.</param>
        /// <returns>Response containing the outcome of the operation.</returns>
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateUSR01(DTOUSR01 objDTOUSR01)
        {
            _usr01Service.Operation = EnmOperation.Create;

            Response response = _usr01Service.PreValidation(objDTOUSR01);
            if (!response.IsError)
            {
                _usr01Service.PreSave(objDTOUSR01);

                response = _usr01Service.Validation();
                if (!response.IsError)
                {
                    response = _usr01Service.Save();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Update the user records.
        /// </summary>
        /// <param name="objDTOUSR01">Updated USR01 model information.</param>
        /// <returns>Response containing the outcome of the operation.</returns>
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateUSR01(DTOUSR01 objDTOUSR01)
        {
            _usr01Service.Operation = EnmOperation.Update;

            Response response = _usr01Service.PreValidation(objDTOUSR01);
            if (!response.IsError)
            {
                _usr01Service.PreSave(objDTOUSR01);

                response = _usr01Service.Validation();
                if (!response.IsError)
                {
                    response = _usr01Service.Save();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all the USR01 model's record information.
        /// </summary>
        /// <returns>Response containing the data.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllUSR01()
        {
            Response response = _usr01Service.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Deletes the user record specified by given id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Response containing the outcome of the operation.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteUSR01(int id)
        {
            Response response = _usr01Service.Delete(id);
            return Ok(response);
        }

        #endregion
    }
}
