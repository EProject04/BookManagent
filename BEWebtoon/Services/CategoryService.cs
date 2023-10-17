using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository= categoryRepository;
        }
        public async Task CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryRepository.CreateCategory(createCategoryDto);   
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<CategoryDto> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task<PagedResult<CategoryDto>> GetCategoryPagination(SeacrhPagingRequest request)
        {
            return await _categoryRepository.GetCategoryPagination(request);
        }

        public async Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryRepository.UpdateCategory(updateCategoryDto);
        }
    }
}
