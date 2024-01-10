using HotelBookingAPI.Data;
using HotelBookingAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    /// <summary>
    /// HotelBookingController manages hotel booking operations through a Web API.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        #region Private Fields

        // ApiContext is used to interact with the underlying database.
        private readonly ApiContext _context;

        #endregion

        #region Constructor

        // Constructor to initialize the controller with an ApiContext.
        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// HTTP PUT endpoint to update an existing hotel booking.
        /// </summary>
        /// <param name="id">Client Unique Id</param>
        /// <param name="booking">Hotel Room & Client Details</param>
        /// <returns>Ok JsonResult</returns>
        [HttpPut]
        public JsonResult PutData(int id, [FromBody] HotelBooking booking)
        {
            // Find the existing booking in the database based on the provided ID.
            var result = _context.Bookings.Find(id);

            // Update the booking details with the new values.
            result.RoomNumber = booking.RoomNumber;
            result.ClientName = booking.ClientName;

            // Save changes to the database.
            _context.SaveChanges();

            // Return a JsonResult with an "Ok" response.
            return new JsonResult(Ok());
        }

        /// <summary>
        /// HTTP POST endpoint for creating or editing a hotel booking.
        /// </summary>
        /// <param name="booking">Hotel Room & Client Details</param>
        /// <returns>Ok JsonResult with booking details.</returns>
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            // Check if the booking ID is 0 to determine if it's a new booking or an edit.
            if (booking.Id == 0)
            {
                // Add a new booking to the database.
                _context.Bookings.Add(booking);
            }
            else
            {
                // Find the existing booking in the database based on the provided ID.
                var bookingInDb = _context.Bookings.Find(booking.Id);

                // If the booking is not found, return a JsonResult with a "Not Found" response.
                if (bookingInDb == null)
                    return new JsonResult(NotFound());

                // Update the existing booking with the new values.
                bookingInDb = booking;
            }

            // Save changes to the database.
            _context.SaveChanges();

            // Return a JsonResult with an "Ok" response, including the created or edited booking.
            return new JsonResult(Ok(booking));
        }

        /// <summary>
        /// HTTP GET endpoint to retrieve details of a specific hotel booking.
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>Hotel</returns>
        [HttpGet]
        public JsonResult Get(int id)
        {
            // Find the booking in the database based on the provided ID.
            var result = _context.Bookings.Find(id);

            // If the booking is not found, return a JsonResult with a "Not Found" response.
            if (result == null)
                return new JsonResult(NotFound());

            // Return a JsonResult with the details of the found booking.
            return new JsonResult(result);
        }

        /// <summary>
        /// HTTP DELETE endpoint to delete a specific hotel booking.
        /// </summary>
        /// <param name="id">Hotel Id</param>
        /// <returns>NoContent Response</returns>
        [HttpDelete]
        public JsonResult DeleteBooking(int id)
        {
            // Find the booking in the database based on the provided ID.
            var result = _context.Bookings.Find(id);

            // If the booking is not found, return a JsonResult with a "Not Found" response.
            if (result == null)
                return new JsonResult(NotFound());

            // Remove the booking from the database and save changes.
            _context.Bookings.Remove(result);
            _context.SaveChanges();

            // Return a JsonResult with a "No Content" response.
            return new JsonResult(NoContent());
        }

        /// <summary>
        /// HTTP GET endpoint to retrieve a list of all hotel bookings.
        /// </summary>
        /// <returns>All Hotel Bookings</returns>
        [HttpGet]
        public JsonResult GetAll()
        {
            // Retrieve all bookings from the database and convert to a list.
            var result = _context.Bookings.ToList();

            // Return a JsonResult with an "Ok" response, including the list of bookings.
            return new JsonResult(Ok(result));
        }

        #endregion
    }
}
