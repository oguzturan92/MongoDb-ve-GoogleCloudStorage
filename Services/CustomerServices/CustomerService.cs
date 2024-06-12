using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDbECommerce.Dtos.CustomerDtos;
using MongoDbECommerce.Entity;
using MongoDbECommerce.Settings;

namespace MongoDbECommerce.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
            _mapper = mapper;
        }
        
        public async Task CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            var entity = _mapper.Map<Customer>(customerDto);
            await _customerCollection.InsertOneAsync(entity);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(i => i.CustomerId == id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var entities = await _customerCollection.Find(i => true).ToListAsync();
            return _mapper.Map<List<ResultCustomerDto>>(entities);
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id)
        {
            var entity = await _customerCollection.Find(i => i.CustomerId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCustomerDto>(entity);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto customerDto)
        {
            var entity = _mapper.Map<Customer>(customerDto);
            await _customerCollection.FindOneAndReplaceAsync(i => i.CustomerId == customerDto.CustomerId, entity);
        }
    }
}