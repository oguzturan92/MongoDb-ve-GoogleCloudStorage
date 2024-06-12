using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDbECommerce.Dtos.OrderDtos;
using MongoDbECommerce.Entity;
using MongoDbECommerce.Settings;

namespace MongoDbECommerce.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;
        public OrderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _orderCollection = database.GetCollection<Order>(_databaseSettings.OrderCollectionName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
            _mapper = mapper;
        }
        
        public async Task CreateOrderAsync(CreateOrderDto orderDto)
        {
            var value = _mapper.Map<Order>(orderDto);
            await _orderCollection.InsertOneAsync(value);
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orderCollection.DeleteOneAsync(i => i.OrderId == id);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var values = await _orderCollection.Find(i => true).ToListAsync();
            foreach (var item in values)
            {
                item.Customer = await _customerCollection.Find(i => item.CustomerId == i.CustomerId).FirstOrDefaultAsync();
            }
            return _mapper.Map<List<ResultOrderDto>>(values);
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(string id)
        {
            var value = await _orderCollection.Find<Order>(i => i.OrderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOrderDto>(value);
        }

        public async Task UpdateOrderAsync(UpdateOrderDto orderDto)
        {
            var value = _mapper.Map<Order>(orderDto);
            await _orderCollection.FindOneAndReplaceAsync(i => i.OrderId == orderDto.OrderId, value);
        }
    }
}