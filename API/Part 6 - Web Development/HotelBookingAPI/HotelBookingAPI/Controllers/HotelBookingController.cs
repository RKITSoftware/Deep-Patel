using HotelBookingAPI.Data;
using HotelBookingAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        [HttpPut]
        public JsonResult PutData(int id, [FromBody]HotelBooking booking)
        {
            var result = _context.Bookings.Find(id);

            result.RoomNumber = booking.RoomNumber;
            result.ClientName = booking.ClientName;

            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        // Create / Edit
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if(booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);

                if (bookingInDb == null)
                    return new JsonResult(NotFound());

                bookingInDb = booking;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(booking));
        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(result);
        }

        // Delete
        [HttpDelete]
        public JsonResult DeleteBooking(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Bookings.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
