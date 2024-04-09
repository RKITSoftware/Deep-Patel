using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling category-related API endpoints.
    /// </summary>
    [RoutePrefix("api/CLCategory")]
    public class CLCAT01Controller : ApiController
    {
        /// <summary>
        /// Instance of Business Logic for helping the controller.
        /// </summary>
        private readonly ICAT01Service _cat01Service;

        /// <summary>
        /// Constructor to initialize the category business logic.
        /// </summary>
        public CLCAT01Controller()
        {
            _cat01Service = new BLCAT01();
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="objDTOCAT01">The DTO object representing the category.</param>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public IHttpActionResult Add(DTOCAT01 objDTOCAT01)
        {
            _cat01Service.PreSave(objDTOCAT01, EnmOperation.Create);

            if (_cat01Service.Validation(out Response response))
            {
                _cat01Service.Save(EnmOperation.Create, out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
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
        [HttpPut]
        [Route("Edit")]
        public IHttpActionResult Edit(DTOCAT01 objCAT01DTO)
        {
            _cat01Service.PreSave(objCAT01DTO, EnmOperation.Update);

            if (_cat01Service.Validation(out Response response))
            {
                _cat01Service.Save(EnmOperation.Update, out response);
            }

            return Ok(response);
        }
    }
}
