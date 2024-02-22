using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Models;
using SchoolManagementAPI.Security;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling admin-related API endpoints.
    /// </summary>
    [RoutePrefix("api/admin")]
    [BearerAuth] // Applying Bearer token authentication to the entire controller
    public class CLAdminController : ApiController
    {
        private BLAdmin _blAdmin;

        /// <summary>
        /// Constructor to initialize the BLAdmin instance.
        /// </summary>
        public CLAdminController()
        {
            _blAdmin = new BLAdmin();
        }

        /// <summary>
        /// API endpoint to add a new admin.
        /// </summary>
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(ADMUSR objADMUSR)
        {
            return _blAdmin.Create(objADMUSR);
        }

        /// <summary>
        /// API endpoint to get all admins.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_blAdmin.GetAll());
        }

        /// <summary>
        /// API endpoint to get an admin by ID.
        /// </summary>
        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(int id)
        {
            ADM01 objAdmin = _blAdmin.Get(id);

            if (objAdmin == null)
                return NotFound();

            return Ok(objAdmin);
        }

        /// <summary>
        /// API endpoint to update file data.
        /// </summary>
        [HttpGet]
        [Route("UpdateFile")]
        public IHttpActionResult UpdateFile()
        {
            BLHelper.FileUpdate();
            return Ok("Data written successfully");
        }

        /// <summary>
        /// API endpoint to delete an admin by ID.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete(int id)
        {
            return _blAdmin.Delete(id);
        }

        /// <summary>
        /// API endpoint to update an admin.
        /// </summary>
        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage UpdateAdmin(ADM01 objAdmin)
        {
            return _blAdmin.Update(objAdmin);
        }
    }
}
