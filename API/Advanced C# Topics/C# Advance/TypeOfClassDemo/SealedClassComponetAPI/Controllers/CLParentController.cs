using SealedClassComponetAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassComponetAPI.Controllers
{
    public class CLParentController : ApiController
    {
        #region Public Fields

        /// <summary>
        /// Parent List stores parent data
        /// </summary>
        public static List<PRT01> parentList = new List<PRT01>();

        #endregion

        #region API Endpoints

        /// <summary>
        /// Get all parents data
        /// </summary>
        /// <returns>parents list data</returns>
        [HttpGet]
        public IHttpActionResult GetAllParents()
        {
            return Ok(parentList);
        }

        /// <summary>
        /// Create a parent
        /// </summary>
        /// <param name="parent">Parent data</param>
        /// <returns>200 Response</returns>
        [HttpPost]
        public IHttpActionResult CreateParent(PRT01 parent)
        {
            parentList.Add(parent);
            return Ok("Created Successfully");
        }

        #endregion
    }
}
