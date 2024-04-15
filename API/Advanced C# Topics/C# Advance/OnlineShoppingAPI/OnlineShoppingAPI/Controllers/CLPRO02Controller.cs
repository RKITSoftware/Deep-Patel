using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller to handle <see cref="PRO02"/> related api endpoints.
    /// </summary>
    [RoutePrefix("api/CLPRO02")]
    public class CLPRO02Controller : ApiController
    {
        /// <summary>
        /// Services for <see cref="CLPRO02Controller"/>.
        /// </summary>
        private readonly IPRO02Service _pro02Service;

        /// <summary>
        /// Constructor to initialize the <see cref="CLPRO02Controller"/>.
        /// </summary>
        public CLPRO02Controller()
        {
            _pro02Service = new BLPRO02Handler();
        }

        /// <summary>
        /// Add the product version 2 into the database.
        /// </summary>
        /// <param name="objPRO02DTO">DTO containing the information about <see cref="PRO02"/>.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IHttpActionResult Add(DTOPRO02 objPRO02DTO)
        {
            _pro02Service.Operation = EnmOperation.A;
            Response response = _pro02Service.PreValidation(objPRO02DTO);

            if (!response.IsError)
            {
                _pro02Service.PreSave(objPRO02DTO);
                response = _pro02Service.Validation();

                if (!response.IsError)
                    _pro02Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes the product of version 2 using product id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("DeleteProductV2")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteProductV2(int id)
        {
            Response response = _pro02Service.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// Gets the all product information.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetProductV2")]
        public IHttpActionResult GetPRO02()
        {
            Response response = _pro02Service.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Updates the sell price of the product.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="sellPrice">Updated sell price of product.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPatch]
        [Route("UpdateSellPrice")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateSellPrice(int id, int sellPrice)
        {
            Response response = _pro02Service.UpdateSellPrice(id, sellPrice);
            return Ok(response);
        }

        /// <summary>
        /// Gets all product information for the admin with all catgeory and supplier data.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetAllProductsInfo")]
        public IHttpActionResult GetAllProductInfo()
        {
            Response response = _pro02Service.GetInformation();
            return Ok(response);
        }
    }
}
