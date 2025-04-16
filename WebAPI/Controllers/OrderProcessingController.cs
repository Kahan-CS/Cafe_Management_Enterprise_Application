using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/admin/orderprocessing")]
    [ApiController]
    [Authorize]
    public class OrderProcessingController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public OrderProcessingController(WebAPIDbContext context)
        {
            _context = context;
        }

        //Endpoint to process the order
        [HttpPost("process")]
        public async Task<ActionResult<OrderResponse>> ProcessOrder([FromBody] OrderRequest orderRequest)
        {
            //Validating the request first
            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
            {
                return BadRequest("Invalid order request.");
            }
            //Calculating the subtotal (sum of item prices)
            decimal subtotal = orderRequest.Items.Sum(item => item.Price);
            decimal tax = subtotal * 0.13m;
            decimal total = subtotal + tax;

            // Saving to DB
            _context.OrderRequests.Add(orderRequest);
            await _context.SaveChangesAsync();

            //Providing the response
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