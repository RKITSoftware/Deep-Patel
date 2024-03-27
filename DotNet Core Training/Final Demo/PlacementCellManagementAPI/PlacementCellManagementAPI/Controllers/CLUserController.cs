﻿using Microsoft.AspNetCore.Mvc;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling user-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUserController : ControllerBase
    {
        /// <summary>
        /// Endpoint to retrieve user data.
        /// </summary>
        /// <returns>Returns an HTTP response indicating success.</returns>
        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok(); // Returns 200 OK status
        }
    }
}
