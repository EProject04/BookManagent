using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;
using BEWebtoon.Requests.BookRequest;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<BookDto>> GetAll();
        Task<BookDto> GetById(int id);
        Task CreateBook(CreateBookDto createBookDto);
        Task UpdateBook(UpdateBookDto updateBookDto);
        Task DeleteBook(int id);
        Task<PagedResult<BookDto>> GetBookPagination(BookRequest request);
    }
}
