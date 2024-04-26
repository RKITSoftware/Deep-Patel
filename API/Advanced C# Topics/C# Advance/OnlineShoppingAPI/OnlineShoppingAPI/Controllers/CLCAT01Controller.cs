using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Controllers.Attribute;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="CAT01"/> opertaions
    /// </summary>
    [RoutePrefix("api/CLCAT01")]
    [Authorize(Roles = "Admin")]
    public class CLCAT01Controller : ApiController
    {
        /// <summary>
        /// Instance of <see cref="ICAT01Service"/>.
        /// </summary>
        private readonly ICAT01Service _cat01Service;

        /// <summary>
        /// Constructor to initialize <see cref="CLCAT01Controller"/>.
        /// </summary>
        public CLCAT01Controller()
        {
            _cat01Service = new BLCAT01Handler();
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="objDTOCAT01">The DTO object representing the category.</param>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public IHttpActionResult Add(DTOCAT01 objDTOCAT01)
        {
            _cat01Service.Operation = EnmOperation.A;
            Response response = _cat01Service.PreValidation(objDTOCAT01);

            if (!response.IsError)
            {
                _cat01Service.PreSave(objDTOCAT01);
                response = _cat01Service.Validation();

                if (!response.IsError)
                    response = _cat01Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            Response response = _cat01Service.DeleteValidation(id);

            if (!response.IsError)
                response = _cat01Service.Delete(id);

            return Ok(response);
        }

        /// <summary>
        /// Edits an existing category.
        /// </summary>
        /// <param name="objCAT01DTO">The DTO object representing the category.</param>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpPut]
        [Route("Edit")]
        [ValidateModel]
        public IHttpActionResult Edit(DTOCAT01 objCAT01DTO)
        {
            _cat01Service.Operation = EnmOperation.E;
            Response response = _cat01Service.PreValidation(objCAT01DTO);

            if (!response.IsError)
            {
                _cat01Service.PreSave(objCAT01DTO);
                response = _cat01Service.Validation();

                if (!response.IsError)
                    response = _cat01Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllCategories()
        {
            Response response = _cat01Service.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpGet]
        [Route("Get/{id}")]
        public IHttpActionResult GetById(int id)
        {
            Response response = _cat01Service.GetById(id);
            return Ok(response);
        }
    }
}