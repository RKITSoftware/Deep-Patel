using SealedClassComponetAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace SealedClassComponetAPI.Controllers
{
    public sealed class CLChildrenController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Stores children data in list
        /// </summary>
        private static List<CHD01> chidlrenList = new List<CHD01>();

        #endregion

        #region API Endpoints

        /// <summary>
        /// Get all children data
        /// </summary>
        /// <returns>children list</returns>
        [HttpGet]
        public IHttpActionResult GetChildrenData()
        {
            return Ok(chidlrenList);
        }

        /// <summary>
        /// Create a children and respected parent
        /// </summary>
        /// <param name="children">Children data</param>
        /// <returns>200 Response</returns>
        [HttpPost]
        public IHttpActionResult CreateChildren(CHD01 children)
        {
            chidlrenList.Add(children);
            CLParentController.parentList.Add(children.D01F04);
            return Ok("Children Created");
        }

        #endregion
    }
}
