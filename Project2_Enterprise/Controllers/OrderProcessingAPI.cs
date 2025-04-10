using Microsoft.AspNetCore.Mvc;
using Project2_Enterprise.Models;

namespace Project2_Enterprise.Controllers
{
 
    public class OrderProcessingAPI
    {
    }
    // this defines the route for the API and marks it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       
        // this is the Endpoint to process an order
        [HttpPost("process")]
        public ActionResult<OrderResponse> ProcessOrder([FromBody] OrderRequest orderRequest)
        {
            // Validating the request
            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
            {
                return BadRequest("Invalid order request.");
            }

            // Calculating subtotal (sum of item prices)
            decimal subtotal = orderRequest.Items.Sum(item => item.Price);
            decimal tax = subtotal * 0.1m; // Assume a 10% tax rate
            decimal total = subtotal + tax;

            // Constructing the response
            var response = new OrderResponse
            {
                OrderId = orderRequest.OrderId,
                Items = orderRequest.Items,
                Subtotal = subtotal,
                Total = total
            };

            return Ok(response);
        }
    }

    
}
