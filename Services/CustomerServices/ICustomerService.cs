using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Dtos.CustomerDtos;

namespace MongoDbECommerce.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task CreateCustomerAsync(CreateCustomerDto customerDto);
        Task UpdateCustomerAsync(UpdateCustomerDto customerDto);
        Task DeleteCustomerAsync(string id);
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id);
    }
}