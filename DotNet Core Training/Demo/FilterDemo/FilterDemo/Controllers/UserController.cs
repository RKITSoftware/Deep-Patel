﻿using FilterDemo.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.Controllers
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(LoggingExceptionFilter))] // Apply custom exception logging filter.
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Gets a greeting message for the admin user.
        /// </summary>
        /// <returns>A string message.</returns>
        [HttpGet("Admin")]
        [AllowAnonymous]
        [CustomResultFilter]
        public string Get()
        {
            throw new Exception("Exception");
        }

        /// <summary>
        /// Gets user information by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An IActionResult containing user information.</returns>
        [HttpGet("{id}")]
        // [Authorize(Roles = "User")] // Authorize access to user role only.
        public IActionResult Get(int id)
        {
            return Ok(new { id });
        }
    }
}
