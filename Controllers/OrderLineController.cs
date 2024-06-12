using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDbECommerce.Dtos.OrderLineDtos;
using MongoDbECommerce.Services.OrderLineServices;

namespace MongoDbECommerce.Controllers
{
    public class OrderLineController : Controller
    {
        private readonly IOrderLineService _orderLineService;
        public OrderLineController(IOrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        [HttpGet]
        public async Task<IActionResult> OrderNewLine(string productId, string orderId)
        {
            var createOrderLine = new CreateOrderLineDto()
            {
                OrderLineCount = 1,
                ProductId = productId,
                OrderId = orderId
            };
            await _orderLineService.CreateOrderLineAsync(createOrderLine);
            return RedirectToAction("OrderDetail", "Order", new { id = orderId});
        }

        public async Task<IActionResult> OrderLineDelete(string orderId, string orderLineId, string productId)
        {
            await _orderLineService.DeleteOrderLineAsync(orderLineId, productId);
            return RedirectToAction("OrderDetail", "Order", new { id = orderId});
        }
    }
}