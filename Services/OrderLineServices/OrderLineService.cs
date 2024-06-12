using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDbECommerce.Dtos.OrderLineDtos;
using MongoDbECommerce.Entity;
using MongoDbECommerce.Settings;

namespace MongoDbECommerce.Services.OrderLineServices
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IMongoCollection<OrderLine> _orderLineCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        public OrderLineService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _orderLineCollection = database.GetCollection<OrderLine>(_databaseSettings.OrderLineCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }
        
        public async Task CreateOrderLineAsync(CreateOrderLineDto orderLineDto)
        {
            var product = await _productCollection.Find<Product>(i => i.ProductId == orderLineDto.ProductId).FirstOrDefaultAsync();
            if (product.ProductStock > 0)
            {
                var orderLine = await _orderLineCollection.Find<OrderLine>(i => i.ProductId == orderLineDto.ProductId && i.OrderId == orderLineDto.OrderId).FirstOrDefaultAsync();
                if (orderLine == null)
                {
                    var value = _mapper.Map<OrderLine>(orderLineDto);
                    await _orderLineCollection.InsertOneAsync(value);
                } else
                {
                    orderLine.OrderLineCount += 1;
                    await _orderLineCollection.FindOneAndReplaceAsync(i => i.OrderLineId == orderLine.OrderLineId, orderLine);
                }
                product.ProductStock -= 1;
                await _productCollection.FindOneAndReplaceAsync(i => i.ProductId == product.ProductId, product);
            }
        }

        public async Task DeleteOrderLineAsync(string id, string productId)
        {
            var product = await _productCollection.Find<Product>(i => i.ProductId == productId).FirstOrDefaultAsync();
            product.ProductStock += 1;
            await _productCollection.FindOneAndReplaceAsync(i => i.ProductId == product.ProductId, product);
            var orderLine = await _orderLineCollection.Find<OrderLine>(i => i.OrderLineId == id).FirstOrDefaultAsync();
            if (orderLine.OrderLineCount == 1)
            {
                await _orderLineCollection.DeleteOneAsync(i => i.OrderLineId == id); 
            } else
            {
                orderLine.OrderLineCount -= 1;
                await _orderLineCollection.FindOneAndReplaceAsync(i => i.OrderLineId == orderLine.OrderLineId, orderLine);
            }
        }

        public async Task<List<OrderDetailResultOrderLineDto>> GetAllOrderDetailOrderLineAsync(string id)
        {
            var values = await _orderLineCollection.Find(i => i.OrderId == id).ToListAsync();
            foreach (var item in values)
            {
                item.Product = await _productCollection.Find(i => item.ProductId == i.ProductId).FirstOrDefaultAsync();
            }
            return _mapper.Map<List<OrderDetailResultOrderLineDto>>(values);
        }

        public Task<List<ResultOrderLineDto>> GetAllOrderLineAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIdOrderLineDto> GetByIdOrderLineAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderLineAsync(UpdateOrderLineDto orderLineDto)
        {
            throw new NotImplementedException();
        }
    }
}