using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Dtos.OrderLineDtos;

namespace MongoDbECommerce.Services.OrderLineServices
{
    public interface IOrderLineService
    {
        Task<List<ResultOrderLineDto>> GetAllOrderLineAsync();
        Task CreateOrderLineAsync(CreateOrderLineDto orderLineDto);
        Task UpdateOrderLineAsync(UpdateOrderLineDto orderLineDto);
        Task DeleteOrderLineAsync(string id, string productId);
        Task<GetByIdOrderLineDto> GetByIdOrderLineAsync(string id);
        Task<List<OrderDetailResultOrderLineDto>> GetAllOrderDetailOrderLineAsync(string id);
    }
}