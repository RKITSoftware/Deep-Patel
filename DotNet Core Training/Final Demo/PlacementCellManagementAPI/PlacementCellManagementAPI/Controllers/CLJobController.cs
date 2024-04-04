using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing job operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLJobController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IJobService"/>
        /// </summary>
        private readonly IJobService _jobService;

        /// <summary>
        /// Constructor for CLJobController.
        /// </summary>
        /// <param name="jobService">The service for job operations.</param>
        public CLJobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <param name="objJobDto">The DTO containing job information.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("")]
        public ActionResult Add(DtoJOB01 objJobDto)
        {
            _jobService.PreSave(objJobDto);

            if (!_jobService.Validation())
                return BadRequest(ModelState);

            return Ok(_jobService.Add());
        }

        /// <summary>
        /// Retrieves all jobs.
        /// </summary>
        /// <returns>The list of all jobs.</returns>
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_jobService.GetAll().ToJson());
        }
    }
}
