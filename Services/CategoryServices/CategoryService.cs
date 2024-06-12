using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDbECommerce.Dtos.CategoryDtos;
using MongoDbECommerce.Entity;
using MongoDbECommerce.Settings;

namespace MongoDbECommerce.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(entity);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(i => i.CategoryId == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var entities = await _categoryCollection.Find(i => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(entities);
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var entity = await _categoryCollection.Find(i => i.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(entity);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(i => i.CategoryId == categoryDto.CategoryId, entity);
        }
    }
}