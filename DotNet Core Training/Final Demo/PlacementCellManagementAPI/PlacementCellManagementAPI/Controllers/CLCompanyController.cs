using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing company operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLCompanyController : ControllerBase
    {
        /// <summary>
        /// The service for company operations.
        /// </summary>
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Constructor for initializing CLCompanyController.
        /// </summary>
        /// <param name="companyService">The service for company operations.</param>
        public CLCompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <param name="objCompanyDto">The DTO containing company information.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("")]
        public ActionResult Add(DtoCMP01 objCompanyDto)
        {
            _companyService.PreSave(objCompanyDto);

            if (!_companyService.Validation())
            {
                return BadRequest();
            }

            return Ok(_companyService.Add());
        }

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>The list of all companies.</returns>
        [HttpGet("All")]
        public ActionResult GetAll()
        {
            return Ok(_companyService.GetAll());
        }

        /// <summary>
        /// Retrieves a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>The company information.</returns>
        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            return Ok(_companyService.Get(id));
        }

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{id:int}")]
        public ActionResult DeleteById(int id)
        {
            return Ok(_companyService.Delete(id));
        }
    }
}
