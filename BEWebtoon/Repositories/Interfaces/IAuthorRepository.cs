using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorDto>> GetAll();
        Task<AuthorDto> GetById(int id);
        Task UpdateAuthor(UpdateAuthorDto updateAuthorDto);
        Task DeleteAuthor(int id);
        Task<PagedResult<AuthorDto>> GetAuthorPagination(SeacrhPagingRequest request);
    }
}
