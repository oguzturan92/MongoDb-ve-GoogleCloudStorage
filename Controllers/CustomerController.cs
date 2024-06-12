using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDbECommerce.Dtos.CustomerDtos;
using MongoDbECommerce.Services.CustomerServices;

namespace MongoDbECommerce.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CustomerCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerCreate(CreateCustomerDto createCustomerDto)
        {
            await _customerService.CreateCustomerAsync(createCustomerDto);
            return RedirectToAction("CustomerList", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> CustomerUpdate(string id)
        {
            var value = await _customerService.GetByIdCustomerAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerUpdate(UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomerAsync(updateCustomerDto);
            return RedirectToAction("CustomerList", "Customer");
        }

        public async Task<IActionResult> CustomerDelete(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList", "Customer");
        }
    }
}