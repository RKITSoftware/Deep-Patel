using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing company operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLCMP01Controller : ControllerBase
    {
        /// <summary>
        /// The service for company operations.
        /// </summary>
        private readonly ICMP01Service _cmp01Service;

        /// <summary>
        /// Constructor for initializing CLCompanyController.
        /// </summary>
        /// <param name="companyService">The service for company operations.</param>
        public CLCMP01Controller(ICMP01Service companyService)
        {
            _cmp01Service = companyService;
        }

        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <param name="objCompanyDto">The DTO containing company information.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("")]
        public ActionResult Add(DTOCMP01 objDTOCMP01)
        {
            _cmp01Service.Operation = EnmOperation.A;
            Response response = _cmp01Service.PreValidation(objDTOCMP01);

            if (!response.IsError)
            {
                _cmp01Service.PreSave(objDTOCMP01);
                response = _cmp01Service.Validation();

                if (!response.IsError)
                {
                    response = _cmp01Service.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>The list of all companies.</returns>
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(_cmp01Service.GetAll());
        }

        /// <summary>
        /// Retrieves a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>The company information.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_cmp01Service.Get(id));
        }

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{id:int}")]
        public ActionResult DeleteById(int id)
        {
            Response response = _cmp01Service.DeleteValidation(id);

            if (!response.IsError)
            {
                response = _cmp01Service.Delete(id);
            }

            return Ok(response);
        }
    }
}
