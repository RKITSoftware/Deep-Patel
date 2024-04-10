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
            _pro02Service = new BLPRO02();
        }

        /// <summary>
        /// Add the product version 2 into the database.
        /// </summary>
        /// <param name="objPRO02DTO">DTO containing the information about <see cref="PRO02"/>.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public IHttpActionResult Add(DTOPRO02 objPRO02DTO)
        {
            _pro02Service.PreSave(objPRO02DTO, EnmOperation.Create);

            if (_pro02Service.Validation(out Response response))
            {
                _pro02Service.Save(out response);
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
        public IHttpActionResult DeleteProductV2(int id)
        {
            _pro02Service.Delete(id, out Response response);
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
            _pro02Service.GetAll(out Response response);
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
        public IHttpActionResult UpdateSellPrice(int id, int sellPrice)
        {
            _pro02Service.UpdateSellPrice(id, sellPrice, out Response response);
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
            _pro02Service.GetInformation(out Response response);
            return Ok(response);
        }
    }
}
