using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
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
            _pro01Service = new BLPRO01();
        }

        /// <summary>
        /// Creates a new product of version 1.
        /// </summary>
        /// <param name="objPRO01DTO">DTO containing the information about PRO01.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateProduct(DTOPRO01 objPRO01DTO)
        {
            _pro01Service.PreSave(objPRO01DTO, EnmOperation.Create);

            if (_pro01Service.Validation(out Response response))
            {
                _pro01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates the product infomration.
        /// </summary>
        /// <param name="objDTOPRO01">DTO containing the updated informtion of <see cref="PRO01"/></param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(DTOPRO01 objDTOPRO01)
        {
            _pro01Service.PreSave(objDTOPRO01, EnmOperation.Update);

            if (_pro01Service.Validation(out Response response))
            {
                _pro01Service.Save(out response);
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
        public IHttpActionResult Delete(int id)
        {
            _pro01Service.Delete(id, out Response response);
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
            _pro01Service.GetAll(out Response response);
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
        public IHttpActionResult UpdateQuantity(int id, int quantity)
        {
            _pro01Service.UpdateQuantity(id, quantity, out Response response);
            return Ok(response);
        }
    }
}
