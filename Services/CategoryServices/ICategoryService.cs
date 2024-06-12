using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Dtos.CategoryDtos;

namespace MongoDbECommerce.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    }
}