using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CustomerClient.Services;
using CustomerClient.ViewModels;

namespace CustomerClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Order/Create
        public IActionResult Create()
        {
            return View(new OrderViewModel());
        }

        // POST: /Order/Create
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdOrder = await _orderService.CreateOrderAsync(model);
                return RedirectToAction("Status", new { id = createdOrder.Id });
            }
            return View(model);
        }

        // GET: /Order/Status/{id}
        public async Task<IActionResult> Status(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return View(order);
        }
    }
}
