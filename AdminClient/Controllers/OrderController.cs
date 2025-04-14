using AdminClient.Entities;
using AdminClient.Messages;
using AdminClient.Services;
using AdminClient.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AdminClient.Controllers
{
	public class OrderController(OrderApiService orderService) : Controller
	{
		private readonly OrderApiService _orderService = orderService;

		// Get all orders
		public async Task<IActionResult> Index()
		{
			try
			{
				var orders = await _orderService.GetAllOrdersAsync();

				if (orders.Count == 0)
				{
					ViewBag.ApiError = "Could not retrieve orders. Please check the API connection.";
				}

				return View(orders);
			}
			catch (Exception ex)
			{
				ViewBag.ApiError = $"Error loading orders: {ex.Message}";
				return View(new List<OrderDto>());
			}
		}

		// Post update to order status
		[HttpPost]
		public async Task<IActionResult> UpdateStatus(int orderId, string newStatus)
		{
			try
			{
				var allOrders = await _orderService.GetAllOrdersAsync();
				var order = allOrders.FirstOrDefault(o => o.OrderId == orderId);

				if (order != null && Enum.TryParse<OrderStatus>(newStatus, out var parsedStatus))
				{
					order.Status = parsedStatus;
					await _orderService.UpdateOrderAsync(orderId, order);
				}
			}
			catch (Exception ex)
			{
				TempData["Error"] = $"Failed to update order status: {ex.Message}";
			}

			return RedirectToAction("Index");
		}

		// Delete order
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _orderService.DeleteOrderAsync(id);
			}
			catch (Exception ex)
			{
				TempData["Error"] = $"Failed to delete order: {ex.Message}";
			}

			return RedirectToAction("Index");
		}
	}
}
