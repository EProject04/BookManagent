using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public async Task DeleteAuthor(int id)
        {
            await _repository.DeleteAuthor(id);
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PagedResult<AuthorDto>> GetAuthorPagination(SeacrhPagingRequest request)
        {
           return await _repository.GetAuthorPagination(request);
        }

        public async Task<AuthorDto> GetById(int id)
        {
            return await _repository.GetById(id); 
        }

        public async Task UpdateAuthor(UpdateAuthorDto updateAuthorDto)
        {
            await _repository.UpdateAuthor(updateAuthorDto);
        }
    }
}
