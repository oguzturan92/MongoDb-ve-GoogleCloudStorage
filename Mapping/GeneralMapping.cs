using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDbECommerce.Dtos.CategoryDtos;
using MongoDbECommerce.Dtos.CustomerDtos;
using MongoDbECommerce.Dtos.OrderDtos;
using MongoDbECommerce.Dtos.OrderLineDtos;
using MongoDbECommerce.Dtos.ProductDtos;
using MongoDbECommerce.Entity;

namespace MongoDbECommerce.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, GetByIdOrderDto>().ReverseMap();

            CreateMap<OrderLine, ResultOrderLineDto>().ReverseMap();
            CreateMap<OrderLine, CreateOrderLineDto>().ReverseMap();
            CreateMap<OrderLine, UpdateOrderLineDto>().ReverseMap();
            CreateMap<OrderLine, GetByIdOrderLineDto>().ReverseMap();
            CreateMap<OrderLine, OrderDetailResultOrderLineDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductDto>().ReverseMap();
        }
    }
}