using AJAXPracticeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AJAXPracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static List<Order> _lstOrder = new();

        [HttpPost]
        public IActionResult Add(Order order)
        {
            _lstOrder.Add(order);
            Response response = new()
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Order successfully created.",
                Data = order
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            Response response = new()
            {
                Data = _lstOrder,
                Message = "Success",
                StatusCode = HttpStatusCode.OK
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _lstOrder.RemoveAll(x => x.Id == id);

            Response response = new()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Order deleted successfully."
            };

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(Order order)
        {
            Response response = new();
            Order? existingOrder = _lstOrder.FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder == null)
            {
                response.IsError = true;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Order not found.";
            }
            else
            {
                existingOrder.Name = order.Name;
                existingOrder.Drink = order.Drink;

                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Order updated successfully.";
            }

            return Ok(response);
        }
    }
}
