using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="CAT01"/> opertaions
    /// </summary>
    [RoutePrefix("api/CLCAT01")]
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
            _cat01Service = new BLCAT01();
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
            _cat01Service.Operation = EnmOperation.Create;

            if (_cat01Service.PreValidation(objDTOCAT01, out Response response))
            {
                _cat01Service.PreSave(objDTOCAT01);

                if (_cat01Service.Validation(out response))
                    _cat01Service.Save(out response);
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
            _cat01Service.GetAll(out Response response);
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
            _cat01Service.GetById(id, out Response response);
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
            _cat01Service.Delete(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Edits an existing category.
        /// </summary>
        /// <param name="objCAT01DTO">The DTO object representing the category.</param>
        /// <returns><see cref="Response"/> indicating the outcome of the operation.</returns>
        [HttpPut]
        [Route("Edit")]
        public IHttpActionResult Edit(DTOCAT01 objCAT01DTO)
        {
            _cat01Service.Operation = EnmOperation.Update;

            if (_cat01Service.PreValidation(objCAT01DTO, out Response response))
            {
                _cat01Service.PreSave(objCAT01DTO);

                if (_cat01Service.Validation(out response))
                    _cat01Service.Save(out response);
            }

            return Ok(response);
        }
    }
}
