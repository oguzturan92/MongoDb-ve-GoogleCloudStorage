using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDbECommerce.Dtos.CustomerDtos;
using MongoDbECommerce.Dtos.OrderDtos;
using MongoDbECommerce.Dtos.ProductDtos;
using MongoDbECommerce.Services.CustomerServices;
using MongoDbECommerce.Services.OrderLineServices;
using MongoDbECommerce.Services.OrderServices;
using MongoDbECommerce.Services.ProductServices;

namespace MongoDbECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLineService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IOrderLineService orderLineService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _orderLineService = orderLineService;
            _customerService = customerService;
            _productService = productService;
        }

        public async Task<IActionResult> OrderList()
        {
            var values = await _orderService.GetAllOrderAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> OrderCreate()
        {
            List<ResultCustomerDto> customerList = await _customerService.GetAllCustomerAsync();
            ViewBag.customerList = customerList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderCreate(CreateOrderDto createOrderDto)
        {
            createOrderDto.OrderDate = DateTime.Now;
            await _orderService.CreateOrderAsync(createOrderDto);
            return RedirectToAction("OrderList", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> OrderUpdate(string id)
        {
            var value = await _orderService.GetByIdOrderAsync(id);
            List<ResultCustomerDto> customerList = await _customerService.GetAllCustomerAsync();
            ViewBag.customerList = customerList;
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> OrderUpdate(UpdateOrderDto updateOrderDto)
        {
            await _orderService.UpdateOrderAsync(updateOrderDto);
            return RedirectToAction("OrderList", "Order");
        }

        public async Task<IActionResult> OrderDelete(string id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction("OrderList", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetail(string id)
        {
            List<ResultProductDto> products = await _productService.GetAllProductAsync();
            ViewBag.products = products;
            ViewBag.orderId = id;
            var values = await _orderLineService.GetAllOrderDetailOrderLineAsync(id);
            return View(values);
        }
    }
}