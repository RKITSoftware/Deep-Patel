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
    /// Controller to handle <see cref="PRO01"/> api endpoints.
    /// </summary>
    [Route("api/CLPRO01")]
    public class CLPRO01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="IPRO01Service"/>.
        /// </summary>
        private readonly IPRO01Service _pro01Service;

        /// <summary>
        /// Constructor to initialize the <see cref="CLPRO01Controller"/>.
        /// </summary>
        public CLPRO01Controller()
        {
            _pro01Service = new BLPRO01Handler();
        }

        /// <summary>
        /// Creates a new product of version 1.
        /// </summary>
        /// <param name="objPRO01DTO">DTO containing the information about PRO01.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IHttpActionResult CreateProduct(DTOPRO01 objPRO01DTO)
        {
            _pro01Service.Operation = EnmOperation.A;
            Response response = _pro01Service.PreValidation(objPRO01DTO);

            if (!response.IsError)
            {
                _pro01Service.PreSave(objPRO01DTO);
                response = _pro01Service.Validation();

                if (!response.IsError)
                    _pro01Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes the product using product id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            Response response = _pro01Service.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// Get all product information from the database.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAll()
        {
            Response response = _pro01Service.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// Updates the product infomration.
        /// </summary>
        /// <param name="objDTOPRO01">DTO containing the updated informtion of <see cref="PRO01"/></param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IHttpActionResult Update(DTOPRO01 objDTOPRO01)
        {
            _pro01Service.Operation = EnmOperation.E;
            Response response = _pro01Service.PreValidation(objDTOPRO01);

            if (!response.IsError)
            {
                _pro01Service.PreSave(objDTOPRO01);
                response = _pro01Service.Validation();

                if (!response.IsError)
                    _pro01Service.Save();
            }

            return Ok(response);
        }
        /// <summary>
        /// Updates the quantity of the product.
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="quantity">Quantity to add.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPatch]
        [Route("UpdateQuantity/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateQuantity(int id, int quantity)
        {
            Response response = _pro01Service.UpdateQuantity(id, quantity);
            return Ok(response);
        }
    }
}
