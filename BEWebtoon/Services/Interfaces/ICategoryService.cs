using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task CreateCategory(CreateCategoryDto createCategoryDto);
        Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategory(int id);
        Task<PagedResult<CategoryDto>> GetCategoryPagination(SeacrhPagingRequest request);
    }
}
