using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDbECommerce.Dtos.CategoryDtos;
using MongoDbECommerce.Dtos.ProductDtos;
using MongoDbECommerce.Entity;
using MongoDbECommerce.Services;
using MongoDbECommerce.Services.CategoryServices;
using MongoDbECommerce.Services.ProductServices;

namespace MongoDbECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICloudStorageService _cloudStorageService;
        public ProductController(IProductService productService, ICategoryService categoryService, ICloudStorageService cloudStorageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            List<ResultCategoryDto> categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.categoryList = categoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(CreateProductDto createProductDto, IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var dosyaYolu = $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyymmddHHmmss")}{extension}";

                await _cloudStorageService.UploadFileAsync(file, dosyaYolu);
                createProductDto.ProductImage = dosyaYolu;
            }
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> ProductUpdate(string id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            List<ResultCategoryDto> categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.categoryList = categoryList;
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(UpdateProductDto updateProductDto, IFormFile file)
        {
            if (file != null)
            {
                var product = await _productService.GetByIdProductAsync(updateProductDto.ProductId);
                await _cloudStorageService.DeleteFileAsync(product.ProductImage);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var dosyaYolu = $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyymmddHHmmss")}{extension}";

                await _cloudStorageService.UploadFileAsync(file, dosyaYolu);
                updateProductDto.ProductImage = dosyaYolu;
            }

            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList", "Product");
        }

        public async Task<IActionResult> ProductDelete(string id)
        {
            var product = await _productService.GetByIdProductAsync(id);
            await _cloudStorageService.DeleteFileAsync(product.ProductImage);
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList", "Product");
        }
    }
}