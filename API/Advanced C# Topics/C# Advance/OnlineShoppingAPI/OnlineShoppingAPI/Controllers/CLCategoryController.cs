using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Filter;
using OnlineShoppingAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling category-related API endpoints.
    /// </summary>
    [RoutePrefix("api/CLCategory")]
    public class CLCategoryController : ApiController
    {
        /// <summary>
        /// Instance of Business Logic for helping the controller.
        /// </summary>
        private BLCategory _category;

        /// <summary>
        /// Constructor to initialize the category business logic.
        /// </summary>
        public CLCategoryController()
        {
            _category = new BLCategory();
        }

        /// <summary>
        /// API endpoint for adding a new category.
        /// </summary>
        /// <param name="objCategory">The category object to be added.</param>
        /// <returns>HttpResponseMessage indicating success or failure.</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public HttpResponseMessage Add(CAT01 objCategory)
        {
            return _category.Add(objCategory);
        }

        /// <summary>
        /// API endpoint for retrieving all categories.
        /// </summary>
        /// <returns>IHttpActionResult with a list of categories 
        /// or NotFound if none are found.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllCategories()
        {
            List<CAT01> lstCategory = _category.GetAll();

            if (lstCategory == null)
                return NotFound();

            return Ok(lstCategory);
        }

        /// <summary>
        /// API Endpoint for retrieving a category.
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Category</returns>
        [HttpGet]
        [Route("Get/{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_category.Get(id));
        }

        /// <summary>
        /// API Endpoint for deleting the category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Response message accoring to the delete.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            return _category.Delete(id);
        }

        /// <summary>
        /// API Endpoint for editing category using Category Id
        /// </summary>
        /// <param name="objCategory">New category information</param>
        [HttpPut]
        [Route("Edit")]
        public HttpResponseMessage Edit(CAT01 objCategory)
        {
            return _category.Edit(objCategory);
        }
    }
}
