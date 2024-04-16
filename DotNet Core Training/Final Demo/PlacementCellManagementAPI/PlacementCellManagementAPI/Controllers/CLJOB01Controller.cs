using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing job operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLJOB01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IJOB01Service"/>
        /// </summary>
        private readonly IJOB01Service _job01Service;

        /// <summary>
        /// Constructor for CLJobController.
        /// </summary>
        /// <param name="jobService">The service for job operations.</param>
        public CLJOB01Controller(IJOB01Service jobService)
        {
            _job01Service = jobService;
        }

        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <param name="objJobDto">The DTO containing job information.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("")]
        public ActionResult Add(DTOJOB01 objJobDto)
        {
            _job01Service.Operation = EnmOperation.A;
            Response response = _job01Service.PreValidation(objJobDto);

            if (!response.IsError)
            {
                _job01Service.PreSave(objJobDto);
                response = _job01Service.Validation();

                if (!response.IsError)
                {
                    response = _job01Service.Save();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all jobs.
        /// </summary>
        /// <returns>The list of all jobs.</returns>
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_job01Service.GetAll());
        }
    }
}
